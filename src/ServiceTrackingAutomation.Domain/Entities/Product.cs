using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ProductClassId { get; set; }

    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    //Virtual
    public virtual ProductType ProductClass { get; set; }


}