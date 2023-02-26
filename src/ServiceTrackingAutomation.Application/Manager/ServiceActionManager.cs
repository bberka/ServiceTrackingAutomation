namespace ServiceTrackingAutomation.Application.Manager;

public class ServiceActionManager : IServiceActionManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ServiceActionManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<ServiceAction> GetServiceActions()
    {
        return _unitOfWork.ServiceActionRepository.ToList();
    }

    public List<ServiceAction> GetServiceActionsByServiceId(int serviceId)
    {
        return _unitOfWork.ServiceActionRepository.Get(x => x.ServiceId == serviceId).ToList();
    }

    public ResultData<ServiceAction> GetServiceActionByComplaintId(int complaintId)
    {
        var serviceAction = _unitOfWork.ServiceActionRepository.Get(x => x.ComplaintId == complaintId).FirstOrDefault();
        if (serviceAction == null)
        {
            return Result.Warn(1, "Servis eylemi bulunamadı");
        }
        return serviceAction;
    }

    public Result AddServiceAction(ServiceAction serviceAction)
    {
        _unitOfWork.ServiceActionRepository.Insert(serviceAction);
        return _unitOfWork.SaveResult(1);
    }

    public ResultData<ServiceAction> GetServiceAction(int id)
    {
        var serviceAction = _unitOfWork.ServiceActionRepository.Get(x => x.Id == id).FirstOrDefault();
        if (serviceAction == null)
        {
            return Result.Warn(1, "Servis eylemi bulunamadı");
        }
        return serviceAction;
    }

    public Result DeleteServiceAction(int id)
    {
        var serviceActionResult = GetServiceAction(id);
        if (serviceActionResult.IsFailure)
        {
            return serviceActionResult.ToResult(100);
        }
        _unitOfWork.ServiceActionRepository.Delete(serviceActionResult.Data);
        return _unitOfWork.SaveResult(2);
    }

    public Result UpdateServiceAction(ServiceAction serviceAction)
    {
        var serviceActionResult = GetServiceAction(serviceAction.Id);
        if (serviceActionResult.IsFailure)
        {
            return serviceActionResult.ToResult(100);
        }
        var serviceActionDb = serviceActionResult.Data;
        serviceActionDb.ComplaintId = serviceAction.ComplaintId;
        serviceActionDb.ServiceId = serviceAction.ServiceId;
        serviceActionDb.Description = serviceAction.Description;
        serviceActionDb.ReceivedFromServiceDate = serviceAction.ReceivedFromServiceDate;
        serviceActionDb.SentToServiceDate = serviceAction.SentToServiceDate;
        _unitOfWork.ServiceActionRepository.Update(serviceActionDb);
        return _unitOfWork.SaveResult(3);
    }
}