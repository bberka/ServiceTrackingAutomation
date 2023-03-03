namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IComplaintManager
{
    List<Complaint> GetComplaints();
    List<Complaint> GetCustomerComplaints(int customerId);
    Result AddComplaint(ComplaintDto data);
    ResultData<Complaint> GetComplaint(int id);
    Result DeleteComplaint(int id);
    Result UpdateComplaint(Complaint complaint);

}