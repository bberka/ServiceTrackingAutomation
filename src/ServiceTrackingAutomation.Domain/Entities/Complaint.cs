using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Complaint : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Müşteri")]
    public int CustomerId { get; set; }

    [MaxLength(1000)]
    [Display(Name = "Açıklama")]
    public string Description { get; set; }


    [Display(Name = "Kayıt Tarihi")]

    public DateTime RegisterDate { get; set; } = DateTime.Now;


    [Display(Name = "Müşteriden Alınma Tarihi")]
    public DateTime? ReceivedFromCustomerDate { get; set; }

    [Display(Name = "Müşteriye Yollama Tarihi")]
    public DateTime? SentToCustomerDate{ get; set; }

    [Display(Name = "Müşteriye Ulaştığı Tarihi")]
    public DateTime? CustomerReceivedDate { get; set; }


    [Display(Name = "Müşterinin Yolladığı Kargo Takip No")]
    [MaxLength(1000)]
    public string? CargoTrackingNumberFromCustomer { get; set; }

    [Display(Name = "Müşteriye Yollanan Kargo Takip No")]
    [MaxLength(1000)]
    public string? CargoTrackingNumberToCustomer { get; set; }

    //Virtual
    public virtual Customer Customer { get; set; }
    public virtual ServiceAction ServiceAction { get; set; }      
    public virtual List<ComplaintProduct> ComplaintProducts { get; set; }


}