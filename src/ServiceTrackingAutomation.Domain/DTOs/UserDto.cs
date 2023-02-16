using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceTrackingAutomation.Domain.DTOs;

public class UserDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [Display(Name = "Kayıt Tarihi")]
    public DateTime RegisterDate { get; set; }

    [EmailAddress]
    [MaxLength(256)]
    [Display(Name = "Email Adresi")]
    public string EmailAddress { get; set; }


    [Display(Name = "Rol Id")]
    public int Role { get; set; }


    [Display(Name = "Rol")]
    public string RoleString
    {
        get
        {
            switch (Role)
            {
                case 1: return "Owner";
                case 2: return "Admin";
                case 3: return "User";
                default: return "Geçersiz";
            }
        }
    }
}