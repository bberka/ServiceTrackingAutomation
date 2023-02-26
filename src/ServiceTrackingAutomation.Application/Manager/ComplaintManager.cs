namespace ServiceTrackingAutomation.Application.Manager;

public class ComplaintManager : IComplaintManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ComplaintManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<Complaint> GetComplaints()
    {
        return _unitOfWork.ComplaintRepository.ToList();
    }

    public List<Complaint> GetCustomerComplaints(int customerId)
    {
        return _unitOfWork.ComplaintRepository.Get(x => x.CustomerId == customerId).ToList();
    }


    public Result AddComplaint(Complaint complaint)
    {
        _unitOfWork.ComplaintRepository.Insert(complaint);
        return _unitOfWork.SaveResult(1);
    }

   

    public ResultData<Complaint> GetComplaint(int id)
    {
        var complaint = _unitOfWork.ComplaintRepository.Get(x => x.Id == id).FirstOrDefault();
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
        complaintDb.CustomerId = complaint.CustomerId;
        complaintDb.CustomerReceivedDate = complaint.CustomerReceivedDate;
        complaintDb.SentToCustomerDate = complaint.SentToCustomerDate;
        complaintDb.ReceivedFromCustomerDate = complaint.ReceivedFromCustomerDate;
        complaintDb.Description = complaint.Description;
        _unitOfWork.ComplaintRepository.Update(complaintDb);
        return _unitOfWork.SaveResult(2);
    }
}