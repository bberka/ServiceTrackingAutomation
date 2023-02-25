using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore;

namespace ServiceTrackingAutomation.Domain.Entities;

public class CustomerContact : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "İletişim Tipi")]
    public int ContactType { get; set; }

    [MaxLength(128)]
    [Display(Name = "İletişim")]
    public string Contact { get; set; }

    [Display(Name = "Geçerlilik")]
    public bool IsValid { get; set; }

    [Display(Name = "Müşteri")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}