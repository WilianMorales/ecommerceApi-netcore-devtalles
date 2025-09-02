using System;
using ecommerceApi_netcore_devtalles.Models;
using ecommerceApi_netcore_devtalles.Models.Dtos;

namespace ecommerceApi_netcore_devtalles.Repository.IRepository;

public interface IUserRepository
{
    ICollection<User> GetUsers();
    User? GetUserById(int id);
    bool IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    Task<User> Register(CreateUserDto createUserDto);
}
