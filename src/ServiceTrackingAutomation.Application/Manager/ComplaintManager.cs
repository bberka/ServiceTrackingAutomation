using System.Runtime.CompilerServices;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Enums;
using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.Application.Manager;

public class ComplaintManager : IComplaintManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerManager _customerManager;

    public ComplaintManager(IUnitOfWork unitOfWork,
        ICustomerManager customerManager)
    {
        _unitOfWork = unitOfWork;
        _customerManager = customerManager;
    }
    public List<Complaint> GetComplaints()
    {
        return _unitOfWork.ComplaintRepository.Get(null, nameof(Customer)).ToList();
    }

    public List<Complaint> GetCustomerComplaints(int customerId)
    {
        return _unitOfWork.ComplaintRepository.Get(x => x.CustomerId == customerId).ToList();
    }


    public Result AddComplaint(ComplaintDto data)
    {
        var complaint = new Complaint()
        {
            CargoTrackingNumberToCustomer = data.CargoTrackingNumberToCustomer,
            CustomerId = data.CustomerId,
            CustomerReceivedDate = data.CustomerReceivedDate,
            Description = data.Description,
            ReceivedFromCustomerDate = DateTime.Now,
            SentToCustomerDate = data.SentToCustomerDate,
            RegisterDate = DateTime.Now
        };
        _unitOfWork.ComplaintRepository.Insert(complaint);
        //foreach (var id in data.ProductIds)
        //{
        //    var cp = new ComplaintProduct()
        //    {
        //        ComplaintId = complaint.Id,
        //        ProductId = id
        //    };
        //    _unitOfWork.ComplaintProductRepository.Insert(cp);
        //}
        return _unitOfWork.SaveResult(1);
    }



    public ResultData<Complaint> GetComplaint(int id)
    {
        var complaint = _unitOfWork.ComplaintRepository.Get(x => x.Id == id, nameof(Customer)).FirstOrDefault();
        if (complaint == null)
        {
            return Result.Warn(1, "Şikayet bulunamadı");
        }
        return complaint;
    }

    public Result DeleteComplaint(int id)
    {
        var complaintResult = GetComplaint(id);
        if (complaintResult.IsFailure)
        {
            return complaintResult.ToResult(100);
        }
        _unitOfWork.ComplaintRepository.Delete(complaintResult.Data);
        return _unitOfWork.SaveResult(2);
    }

    public Result UpdateComplaint(Complaint complaint)
    {
        var complaintResult = GetComplaint(complaint.Id);
        if (complaintResult.IsFailure)
        {
            return complaintResult.ToResult(100);
        }
        var complaintDb = complaintResult.Data;
        complaintDb.Description = complaint.Description;
        complaintDb.Note = complaint.Note;
        _unitOfWork.ComplaintRepository.Update(complaintDb);
        return _unitOfWork.SaveResult(2);
    }

    public Result CreateComplaintAndCustomer(CreateComplaintAndCustomerModel model)
    {
        var lastId = _unitOfWork.CustomerRepository.GetFirstOrDefaultSelect(x => x.Id, null, x => x.OrderByDescending(y => y.RegisterDate));
        model.Customer.IsValid = true;
        _unitOfWork.CustomerRepository.Insert(model.Customer);
        var complaint = new Complaint()
        {
            CargoTrackingNumberToCustomer = model.ComplaintDto.CargoTrackingNumberToCustomer,
            CustomerId = lastId + 1,
            CustomerReceivedDate = model.ComplaintDto.CustomerReceivedDate,
            Description = model.ComplaintDto.Description,
            ReceivedFromCustomerDate = DateTime.Now,
            SentToCustomerDate = model.ComplaintDto.SentToCustomerDate,
            RegisterDate = DateTime.Now,
            Note = model.ComplaintDto.Note
        };
        var complaintResult = _unitOfWork.SaveResult(1);
        if (complaintResult.IsFailure) return complaintResult;
        _unitOfWork.ComplaintRepository.Insert(complaint);
        return _unitOfWork.SaveResult(1);

    }

    public Result SentToCustomer(SentToCustomerModel model)
    {
        var complaintResult = GetComplaint(model.ComplaintId);

        if (complaintResult.IsFailure)
        {
            return complaintResult.ToResult(100);
        }
        var complaint = complaintResult.Data;
        if (complaint.ComplaintStatusEnum == ComplaintStatus.SentToService || complaint.ComplaintStatusEnum == ComplaintStatus.SentToCustomer)
        {
            return Result.Warn(1, "Şikayet durumu geçersiz");
        }
        complaint.SentToCustomerDate = DateTime.Now;
        complaint.CargoTrackingNumberToCustomer = model.CargoTrackingNumberToCustomer;
        complaint.Status = (int)ComplaintStatus.SentToCustomer;
        _unitOfWork.ComplaintRepository.Update(complaint);
        return _unitOfWork.SaveResult(1);

    }

    public Result SentToService(ServiceUpdateModel model)
    {
        var complaintResult = GetComplaint(model.ComplaintId);
        if (complaintResult.IsFailure)
        {
            return complaintResult.ToResult(100);
        }
        var complaint = complaintResult.Data;
        if (complaint.ComplaintStatusEnum != ComplaintStatus.ReceivedFromCustomer)
        {
            return Result.Warn(1, "Şikayet durumu geçersiz");
        }
        complaint.SentToServiceDate = DateTime.Now;
        complaint.Status = (int)ComplaintStatus.SentToService;
        complaint.ServiceNote = model.ServiceNote;
        _unitOfWork.ComplaintRepository.Update(complaint);
        return _unitOfWork.SaveResult(2);
    }

    public Result ReceivedFromService(ServiceUpdateModel model)
    {
        var complaintResult = GetComplaint(model.ComplaintId);
        if (complaintResult.IsFailure)
        {
            return complaintResult.ToResult(100);
        }
        var complaint = complaintResult.Data;
        if (complaint.ComplaintStatusEnum != ComplaintStatus.SentToService)
        {
            return Result.Warn(1, "Şikayet durumu geçersiz");
        }
        complaint.ReceivedFromServiceDate = DateTime.Now;
        complaint.Status = (int)ComplaintStatus.ReceivedFromService;
        complaint.ServiceNote = model.ServiceNote;
        _unitOfWork.ComplaintRepository.Update(complaint);
        return _unitOfWork.SaveResult(2);
    }
 

   
   
}