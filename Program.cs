using Serilog;
using Serilog.Events;
using Microsoft.Extensions.DependencyInjection;
using P5_Frontend_Car_App.Interfaces;
using P5_Frontend_Car_App.Services;

namespace P5_Frontend_Car_App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithThreadId()
               .WriteTo.Console()

               .WriteTo.File("logs/info-.txt",
                   rollingInterval: RollingInterval.Day,
                   restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate:
                "{Timestamp:HH:mm:ss} | {Level:u3} | [Thread:{ThreadId}] | {SourceContext} | {Message:lj}{NewLine}{Exception}{NewLine}------------------------{NewLine}"
                   )

               .WriteTo.File(
                "logs/error-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error,
                outputTemplate:
                "{Timestamp:HH:mm:ss} | {Level:u3} | [Thread:{ThreadId}] | {SourceContext} | {Message:lj}{NewLine}{Exception}{NewLine}------------------------{NewLine}"
               )

               .CreateLogger();

            try
            {
                Application.ThreadException += (sender, args) =>
                {
                    Log.Error(args.Exception, "Unhandled UI exception");
                    MessageBox.Show("Unexpected error occurred.");
                };

                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    Log.Fatal((Exception)args.ExceptionObject, "Fatal error");
                };

                ApplicationConfiguration.Initialize();

                var services = new ServiceCollection();

                services.AddHttpClient<IApiService, ApiService>();

                services.AddTransient<Main_Form>();

                var provider = services.BuildServiceProvider();

                Application.Run(
                    provider.GetRequiredService<Main_Form>());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application crashed on startup");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}