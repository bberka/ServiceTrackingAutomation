namespace ServiceTrackingAutomation.Domain.Models;

public class CreateComplaintAndCustomerModel
{
    public ComplaintDto ComplaintDto { get; set; }
    public Customer Customer { get; set; }
}