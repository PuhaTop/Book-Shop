using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookShop.Admin.Model;

public class Login
{
    [Required(ErrorMessage = "User Name is empty")]
    [StringLength(20,ErrorMessage = "Name is too long")]
    [MinLength(4,ErrorMessage = "Name is too short")]
    public string UserName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is empty")]
    [MinLength(6,ErrorMessage = "Password min length 6 symbols")]
    public string Password { get; set; } = string.Empty;
}