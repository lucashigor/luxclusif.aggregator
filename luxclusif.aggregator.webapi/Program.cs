using luxclusif.aggregator.kernel.Extensions;
using luxclusif.aggregator.webapi;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<WorkerUser>(); 
                services.AddHostedService<WorkerOrder>();
            });
}