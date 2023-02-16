using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Entities;

public class CustomerContact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ContactType { get; set; }

    [MaxLength(128)]
    public string Contact { get; set; }
    public bool IsValid { get; set; }
}