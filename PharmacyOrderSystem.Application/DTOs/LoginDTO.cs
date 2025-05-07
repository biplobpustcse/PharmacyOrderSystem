using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOrderSystem.Application.DTOs
{
    public class LoginDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
