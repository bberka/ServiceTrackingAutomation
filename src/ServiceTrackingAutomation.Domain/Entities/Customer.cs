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
    public string Name { get; set; }
    [Display(Name = "Müşteri Adresi")]
    public string Address { get; set; }

    [Display(Name = "Geçerlilik")]
    public bool IsValid { get; set; }

    [Display(Name = "Kayıt Tarihi")]
    public DateTime RegisterDate { get; set; } = DateTime.Now;
}