using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceTrackingAutomation.Domain.Models;

public class CreateComplaintModel 
{
    public List<Customer> Customers { get; set; } = new List<Customer>();
    public ComplaintDto Dto { get; set; }
}