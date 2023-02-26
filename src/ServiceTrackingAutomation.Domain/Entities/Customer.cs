using EasMe.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Customer : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Müşteri Adı")]
    [MaxLength(100)]
    public string Name { get; set; }
    [Display(Name = "Adres")]
    [MaxLength(1000)]
    public string Address { get; set; }

    [Display(Name = "Telefon No")]
    [MaxLength(15)]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Email Adresi")]
    [MaxLength(255)]
    [EmailAddress]
    public string? EmailAddress { get; set; }


    [Display(Name = "Geçerlilik")]
    public bool IsValid { get; set; }

    [Display(Name = "Kayıt Tarihi")]
    public DateTime RegisterDate { get; set; } = DateTime.Now;


    //Virtual

    public virtual List<Complaint> Complaints { get; set; } = new List<Complaint>();
}