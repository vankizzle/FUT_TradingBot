using FUTAutomationAPI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUTAutomationAPI.Interfaces
{
    public interface IFutAPI
    {
        void Init();
        LoginPage LoginPage { get;}
    }
}
