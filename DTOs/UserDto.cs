using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_Frontend_Car_App.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;        

        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
