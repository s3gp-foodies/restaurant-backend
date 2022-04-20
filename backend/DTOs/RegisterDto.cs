namespace foodies_app.DTOs;
using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Role { get; set; }
    [Required]
    [StringLength(16, MinimumLength = 4)]
    public string Password { get; set; }
}