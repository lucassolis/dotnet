using crypto.Domain;
using MatthiWare.CommandLine;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace crypto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.Console()
                .CreateLogger();

            var options = new CommandLineParserOptions
            {
                AppName = "crypto",
                PrefixShortOption = "-",
                PrefixLongOption = "--"
            };

            var parser = new CommandLineParser<Parameters>(options);

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Log.Error("Erro ao ler os parâmetros");

                foreach (var ex in result.Errors)
                    Log.Error(ex.Message);

                return;
            }

            var command = new Command();
            command.Execute(result.Result);
        }
    }
}
