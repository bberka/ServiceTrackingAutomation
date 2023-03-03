using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceTrackingAutomation.Domain.Models;

public class CreateServiceActionModel
{
    [Display(Name = "Şikayet No")]
    public int ComplaintId { get; set; }

    [Display(Name = "Servis No")]
    public int ServiceId { get; set; }

    [MaxLength(1000)]
    [Display(Name = "Açıklama")]
    public string Description { get; set; } 

    [Display(Name = "Servise Yollama Tarihi")]
    public DateTime? SentToServiceDate { get; set; }

    [Display(Name = "Servisten Gelme Tarihi")]
    public DateTime? ReceivedFromServiceDate { get; set; }
    public List<Service> Services { get; set; } = new List<Service>();

    public ServiceAction ToServiceAction()
    {
        return new ServiceAction()
        {
            ComplaintId = this.ComplaintId,
            ServiceId = this.ServiceId,
            Description = this.Description,
            SentToServiceDate = this.SentToServiceDate,
            ReceivedFromServiceDate = this.ReceivedFromServiceDate
        };
    }
}