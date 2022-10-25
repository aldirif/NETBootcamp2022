using Microsoft.EntityFrameworkCore;
using MyBackendApp.DAL;

namespace MyBackendApp.DTO
{
    public class AddUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}