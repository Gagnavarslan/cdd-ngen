using Microsoft.Owin.Cors;
using Owin;

namespace CoreData.Common.Messaging
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
