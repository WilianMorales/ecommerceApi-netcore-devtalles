using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerceApi_netcore_devtalles.Models.Dtos;

public class CreateUserDto
{
    [Required(ErrorMessage = "El campo usuario es requerido")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "El campo nombre es requerido")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "El campo contraseña es requerido")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "El campo rol es requerido")]
    public string? Role { get; set; }
}
