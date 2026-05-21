using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P5_Frontend_Car_App.Types;

namespace P5_Frontend_Car_App.DTOs.User
{
    public class LoginResponseDto
    {
        public string Username { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string Token { get; set; } = string.Empty; // important for next step
    }
}
