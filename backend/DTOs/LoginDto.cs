using System.ComponentModel.DataAnnotations;

namespace foodies_app.DTOs;

public class LoginDto
{
    
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
