﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrackingAutomation.Domain.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsValid { get; set; }
    public DateTime RegisterDate { get; set; }
}