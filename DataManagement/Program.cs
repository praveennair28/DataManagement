using System.Diagnostics.CodeAnalysis;
using Serilog;



namespace DataManagement
{
    [ExcludeFromCodeCoverage]
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
                    webBuilder.ConfigureLogging(Logger)
                    .UseStartup<Startup>();
                });



        public static void Logger(WebHostBuilderContext ctx, ILoggingBuilder logging)
        {
            if (ctx == null || logging == null)
            {
                return;
            }
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddAzureWebAppDiagnostics();
        }

    }
}

