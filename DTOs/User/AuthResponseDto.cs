using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_Frontend_Car_App.DTOs.User
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto Data { get; set; }
    }
}
