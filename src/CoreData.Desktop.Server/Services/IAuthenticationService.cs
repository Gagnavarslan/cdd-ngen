using CoreData.Desktop.Server.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Services
{
    // https://github.com/dotnetjt/StackifySandbox.Net/blob/a80adc5dca49c4c19335ce8060d13ea4f9bb6f83/HelloStackify.Web/HelloStackify.Web/Controllers/ManageController.cs#L318
    // Single Sign On using ASP.NET Identity between 2 ASP.NET MVC projects https://stackoverflow.com/questions/42229847/single-sign-on-using-asp-net-identity-between-2-asp-net-mvc-projects
    public class Authenticator : IAuthenticationService
    {
        public bool Connect(ConnectionInfo coreData) => throw new NotImplementedException();
    }

    public interface IAuthenticationService
    {
        bool Connect(ConnectionInfo coreData);
    }

    //public 

    //public interface IAuthenticationService
    //{

    //}
}
