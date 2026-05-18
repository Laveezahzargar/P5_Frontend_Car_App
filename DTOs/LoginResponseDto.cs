using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P5_Frontend_Car_App.Types;

namespace P5_Frontend_Car_App.DTOs
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
