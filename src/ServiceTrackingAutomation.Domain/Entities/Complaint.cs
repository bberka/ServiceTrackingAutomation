using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore;
using ServiceTrackingAutomation.Domain.Enums;
using ServiceTrackingAutomation.Domain.Extensions;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Complaint : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Müşteri")]
    public int CustomerId { get; set; }

    [MaxLength(5000)]
    [Display(Name = "Açıklama")]
    public string Description { get; set; }


    [MaxLength(5000)]
    [Display(Name = "Şikayet Notu")]
    public string? Note { get; set; }

    [Display(Name = "Kayıt Tarihi")]
    public DateTime RegisterDate { get; set; } = DateTime.Now;

    [Display(Name = "Şikayet Durumu")]
    public int Status { get; set; } = (int)ComplaintStatus.ReceivedFromCustomer;
    [Display(Name = "Müşteriden Alınma Tarihi")]
    public DateTime? ReceivedFromCustomerDate { get; set; }

    [Display(Name = "Müşteriye Yollama Tarihi")]
    public DateTime? SentToCustomerDate{ get; set; }

    [Display(Name = "Müşteriye Ulaştığı Tarihi")]
    public DateTime? CustomerReceivedDate { get; set; }

    

    [Display(Name = "Müşteriye Yollanan Kargo Takip No")]
    [MaxLength(1000)]
    public string? CargoTrackingNumberToCustomer { get; set; }


    [Display(Name = "Servis No")]
    public int? ServiceId { get; set; }

    [MaxLength(5000)]
    [Display(Name = "Servis Notu")]
    public string? ServiceNote { get; set; }

    [Display(Name = "Servise Yollama Tarihi")]
    public DateTime? SentToServiceDate { get; set; }

    [Display(Name = "Servisten Gelme Tarihi")]
    public DateTime? ReceivedFromServiceDate { get; set; }

    //Virtual
    public virtual Service? Service { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual List<ComplaintProduct> ComplaintProducts { get; set; }


    //No Set
    public ComplaintStatus ComplaintStatusEnum => (ComplaintStatus)Status;
    public string ComplaintStatusMessage => ((ComplaintStatus)Status).ToMessage();
}