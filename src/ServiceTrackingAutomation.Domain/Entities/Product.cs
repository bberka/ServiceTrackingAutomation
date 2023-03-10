using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Product : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Geçerlilik")]
    public bool IsValid { get; set; }


    [MaxLength(128)]
    [Display(Name = "Ürün Adı")]
    public string Name { get; set; }

    [MaxLength(128)]
    [Display(Name = "Ürün Kodu")]
    public string? Code { get; set; }

    [MaxLength(1000)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }
    

}