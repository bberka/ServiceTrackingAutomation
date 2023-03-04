using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceTrackingAutomation.Domain.Models;

public class ServiceUpdateModel
{
    [Display(Name = "Şikayet No")]
    public int ComplaintId { get; set; }

    [Display(Name = "Servis Notu")]
    [MaxLength(5000)]
    public string? ServiceNote { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }
    [Display(Name = "Şikayet Notu")]
    public string? Note { get; set; }
}