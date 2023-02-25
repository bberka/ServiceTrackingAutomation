using EasMe.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrackingAutomation.Domain.Entities;

public class ChangeLog : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Kayıt Tarihi")]
    public DateTime RegisterDate { get; set; } = DateTime.Now;

    [MaxLength(1000)]
    [Display(Name = "Tablo Adı")]
    public string EntityName { get; set; }
    [MaxLength(1000)]
    [Display(Name = "Sütun Adı")]
    public string PropertyName { get; set; }
    [MaxLength(1000)]
    [Display(Name = "Eski Değer")]
    public string? OldValue { get; set; }
    [MaxLength(1000)]
    [Display(Name = "Yeni Değer")]
    public string? NewValue { get; set; }
}