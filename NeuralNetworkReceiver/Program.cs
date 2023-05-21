using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeuralNetworkCore.Models.Settings;
using NeuralNetworkDatabase;
using NeuralNetworkReceiver.Common.Mapper;
using NeuralNetworkReceiver.Receivers;

namespace NeuralNetworkReceiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config =  new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("NeuralNetworkEf");

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddAutoMapper(typeof(NnProfile))
                .AddScoped<IReceiver, Receiver>()
                .AddScoped<ISymptomsService, SymptomsService>()
                .AddScoped<IUserService, UserService>()
                .Configure<RabbitMqSettings>(config.GetSection(nameof(RabbitMqSettings)))
                .AddDbContext<NeuralNetworkDbContext>(options =>
                    options.UseSqlServer(connectionString)
                )
                .BuildServiceProvider();
            
            var receiver = serviceProvider.GetRequiredService<IReceiver>();
            receiver.Receive();
            Console.ReadKey();
        }
        
    }
}