using ServiceTrackingAutomation.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.DTOs;

public class ServiceDto
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Address { get; set; }

    [MaxLength(15)]
    public string PhoneNumber { get; set; }

}