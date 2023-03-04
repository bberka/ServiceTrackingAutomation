using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceTrackingAutomation.Domain.Models;

public class SentToCustomerModel
{
    [Display(Name = "Şikayet No")]
    public int ComplaintId { get; set; }

    [Display(Name = "Müşteriye Yollanan Kargo Takip No")]
    [MaxLength(1000)]
    public string? CargoTrackingNumberToCustomer { get; set; }
}