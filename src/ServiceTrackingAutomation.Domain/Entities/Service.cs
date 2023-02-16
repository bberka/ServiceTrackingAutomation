using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [MaxLength(128)]
    public string Name { get; set; }


    [MaxLength(1000)]
    public string Address { get; set; }


    [MaxLength(15)]
    public string PhoneNumber { get; set; }

    public bool IsValid { get; set; }

    public ServiceAction ServiceAction { get; set; }
}