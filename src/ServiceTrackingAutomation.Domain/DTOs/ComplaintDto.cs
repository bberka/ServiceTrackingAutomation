using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceTrackingAutomation.Domain.DTOs;

public class ComplaintDto
{

    [Display(Name = "Müşteri")]
    public int CustomerId { get; set; }

    [MaxLength(5000)]
    [Display(Name = "Açıklama")]
    public string Description { get; set; }
    
    [Display(Name = "Ürünlerin Müşteriden Alınma Tarihi")]
    public DateTime? ReceivedFromCustomerDate { get; set; }

    [Display(Name = "Ürünlerin Müşteriye Yollama Tarihi")]
    public DateTime? SentToCustomerDate { get; set; }

    [Display(Name = "Ürünlerin Müşteriye Ulaştığı Tarihi")]
    public DateTime? CustomerReceivedDate { get; set; }
    
    [Display(Name = "Müşteriye Yollanan Kargo Takip No")]
    [MaxLength(1000)]
    public string? CargoTrackingNumberToCustomer { get; set; }

    //public List<int> ProductIds { get; set; }

}