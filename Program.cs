using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P5_Frontend_Car_App.Interfaces;
using P5_Frontend_Car_App.Services;
using Serilog;
using Serilog.Events;

namespace P5_Frontend_Car_App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logPath);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .WriteTo.Console()
                .WriteTo.File(
                    Path.Combine(logPath, "info-.txt"),
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate:
                    "{Timestamp:HH:mm:ss} | {Level:u3} | [Thread:{ThreadId}] | {SourceContext} | {Message:lj}{NewLine}{Exception}{NewLine}------------------------{NewLine}"
                )
                .WriteTo.File(
                    Path.Combine(logPath, "error-.txt"),
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

                var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                services.AddSingleton<IConfiguration>(config);

                services.AddHttpClient<IApiService, ApiService>();

                var provider = services.BuildServiceProvider();

                // Resolve only the service
                var apiService = provider.GetRequiredService<IApiService>();

                // Pass role manually
                Application.Run(new SignUp_Form(apiService)); // or "customer"
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