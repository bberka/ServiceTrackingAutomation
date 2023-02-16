using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Entities;

public class ServiceAction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ComplaintId { get; set; }
    public int ServiceId { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    public DateTime? SentToServiceDate { get; set; }
    public DateTime? ReceivedFromServiceDate { get; set; }


    //Virtual
    public virtual Complaint Complaint { get; set; }
    public virtual Service Service { get; set; }
}