using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerceApi_netcore_devtalles.Models.Dtos;

public class UserLoginDto
{
    [Required(ErrorMessage = "El campo usuario es requerido")]
    public string? Username { get; set; }
    
    [Required(ErrorMessage = "El campo contraseña es requerido")]
    public string? Password { get; set; }
}
