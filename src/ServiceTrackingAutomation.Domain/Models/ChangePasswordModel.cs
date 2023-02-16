using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Models;

public class ChangePasswordModel
{
    [MaxLength(64)]
    [MinLength(3)]
    public string OldPassword { get; set; }
    [MaxLength(64)]
    [MinLength(3)]
    public string NewPassword { get; set; }
    [MaxLength(64)]
    [MinLength(3)]
    public string NewPasswordConfirm { get; set; }
}