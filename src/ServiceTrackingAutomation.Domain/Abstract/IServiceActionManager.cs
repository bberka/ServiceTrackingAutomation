namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IServiceActionManager
{
    List<ServiceAction> GetServiceActions();
    List<ServiceAction> GetServiceActionsByServiceId(int serviceId);
    ResultData<ServiceAction> GetServiceActionByComplaintId(int complaintId);
    Result AddServiceAction(ServiceAction serviceAction);
    ResultData<ServiceAction> GetServiceAction(int id);
    Result DeleteServiceAction(int id);
    Result UpdateServiceAction(ServiceAction serviceAction);


}