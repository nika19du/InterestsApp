using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InYourInterest.Areas.Identity.IdentityHostingStartup))]
namespace InYourInterest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}