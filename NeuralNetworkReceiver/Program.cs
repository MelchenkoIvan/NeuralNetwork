using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NeuralNetworkDatabase;
using NeuralNetworkReceiver.Receivers;

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddScoped<IReceiver, FeedForwardReceiver>()
    .AddScoped<IReceiver, RecurrentReceiver>()
    .AddScoped<IUserService, UserService>()
    // .AddDbContext<NeuralNetworkDbContext>(options =>
    //     options.UseSqlServer(connectionString)
    // )
    .BuildServiceProvider();

var receiver = serviceProvider.GetService<IReceiver>();
receiver.Receive();