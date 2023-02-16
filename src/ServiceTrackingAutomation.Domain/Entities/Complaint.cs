using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Complaint
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    public DateTime RegisterDate { get; set; } = DateTime.Now;

    public DateTime? ReceivedFromCustomerDate { get; set; }

    public DateTime? SentToCustomerDate{ get; set; }

    public bool IsDeliveredToCustomer { get; set; } = false;

    public DateTime? CustomerReceivedDate { get; set; }

    //Virtual
    public virtual Customer Customer { get; set; }
}