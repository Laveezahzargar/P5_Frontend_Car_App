using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P5_Frontend_Car_App.Types;

namespace P5_Frontend_Car_App.DTOs.User
{
    public class RegisterRequestDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }
}
