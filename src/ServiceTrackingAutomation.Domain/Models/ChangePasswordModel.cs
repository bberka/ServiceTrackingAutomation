using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Models;

public class ChangePasswordModel
{
    [MaxLength(64)]
    [MinLength(3)]
    [Display(Name = "Şifre")]
    public string OldPassword { get; set; }
    [MaxLength(64)]
    [MinLength(3)]
    [Display(Name = "Yeni Şifre")]

    public string NewPassword { get; set; }
    [MaxLength(64)]
    [MinLength(3)]
    [Display(Name = "Yeni Şifre Tekrar")]
    public string NewPasswordConfirm { get; set; }
}