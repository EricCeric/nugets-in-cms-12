using System.ComponentModel.DataAnnotations;

namespace nugets_in_cms_12.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
