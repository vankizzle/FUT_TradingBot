using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUTAutomationAPI.Interfaces
{
    public interface ILoginOptions
    {
        string Email { get; set; }
        string Password { get; set; }
        string Token { get; set; }
        int? Code { get; set; } 
    }
}
