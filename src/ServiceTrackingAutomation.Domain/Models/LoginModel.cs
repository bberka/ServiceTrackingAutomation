using System.ComponentModel.DataAnnotations;

namespace ServiceTrackingAutomation.Domain.Models;

public class LoginModel
{
    [MaxLength(64)]
    [MinLength(3)]
    public string EmailAddress { get; set; }


    [MaxLength(64)]
    [MinLength(3)]
    public string Password { get; set; }

}