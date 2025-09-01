using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTO.Request;

public class LoginRequestDto
{
    [Required(ErrorMessage = "El campo Email es obligatorio")]
    [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El campo Password es obligatorio")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = null!;
}