using System;

namespace ecommerceApi_netcore_devtalles.Models.Dtos;

public class UserLoginResponseDto
{
    public UserRegisterDto? User { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}
