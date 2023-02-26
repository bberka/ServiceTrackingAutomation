using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Service : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [Display(Name = "Kayıt Tarihi")]
    public DateTime RegisterDate { get; set; } = DateTime.Now;

    [MaxLength(128)]
    [Display(Name = "Adı")]
    public string Name { get; set; }


    [MaxLength(1000)]
    [Display(Name = "Adres")]
    public string Address { get; set; }


    [MaxLength(15)]
    [Display(Name = "Telefon No")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Geçerlilik")]
    public bool IsValid { get; set; }

    //Virtual
    public virtual List<ServiceAction>? ServiceActions { get; set; }
}