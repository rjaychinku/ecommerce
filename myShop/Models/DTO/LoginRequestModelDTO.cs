using System.ComponentModel.DataAnnotations;

public class LoginRequestModelDTO
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
