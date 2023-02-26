namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IComplaintManager
{
    List<Complaint> GetComplaints();
    List<Complaint> GetCustomerComplaints(int customerId);
    Result AddComplaint(Complaint complaint);
    ResultData<Complaint> GetComplaint(int id);
    Result DeleteComplaint(int id);
    Result UpdateComplaint(Complaint complaint);

}