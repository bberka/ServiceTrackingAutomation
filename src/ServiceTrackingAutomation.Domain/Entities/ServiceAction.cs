using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore;

namespace ServiceTrackingAutomation.Domain.Entities;

public class ServiceAction : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

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

    //Virtual
    public virtual Complaint Complaint { get; set; }
    public virtual Service Service { get; set; }
}