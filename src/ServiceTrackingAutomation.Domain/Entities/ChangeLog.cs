using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrackingAutomation.Domain.Entities;

public class ChangeLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime RegisterDate { get; set; } = DateTime.Now;

    [MaxLength(1000)]
    public string EntityName { get; set; }
    [MaxLength(1000)]
    public string PropertyName { get; set; }
    [MaxLength(1000)]
    public string? OldValue { get; set; }
    [MaxLength(1000)]
    public string? NewValue { get; set; }
}