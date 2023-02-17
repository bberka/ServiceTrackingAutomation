using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using EasMe.EntityFrameworkCore;

namespace ServiceTrackingAutomation.Domain.Entities;

[PrimaryKey(nameof(ComplaintId),nameof(ProductId))]
public class ComplaintProduct : IEntity
{
    public int ComplaintId { get; set; }
    public int ProductId { get; set; }


    //Virtual
    public virtual Complaint Complaint { get; set; }
    public virtual Product Product { get; set; }

}