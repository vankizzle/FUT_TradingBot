using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUTAutomationAPI.Interfaces.Pages
{
    interface ILoginPage
    {
        void GoTo();     
        void Login(ILoginOptions options);
    }
}
