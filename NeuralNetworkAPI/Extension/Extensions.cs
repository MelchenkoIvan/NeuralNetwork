using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeuralNetworkCore;
using NeuralNetworkCore.Mapper;
using NeuralNetworkCore.Models.Settings;
using NeuralNetworkDatabase;

namespace NeuralNetworkAPI.Extension
{
	public static class Extensions
	{
		public static IServiceCollection AddCore(this IServiceCollection services, WebApplicationBuilder builder) =>
			services.AddScoped<IUserRepositroy, UserRepository>()
				.AddScoped<INeuralNetworkRepository, NeuralNetworkRepository>()
				.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)))
				.AddAutoMapper(typeof(UserProfile), typeof(NNProfile));
		

		public static IServiceCollection AddServices(this IServiceCollection services) =>
			services.AddScoped<IUserService, UserService>()
				.AddScoped<ISymptomsService, SymptomsService>();

		public static IServiceCollection AddDbContext(this IServiceCollection services, WebApplicationBuilder builder)
		{
			var connectionString = builder.Configuration.GetConnectionString("NeuralNetworkEf");
			services.AddDbContext<NeuralNetworkDbContext>(options =>
					options.UseSqlServer(connectionString)
				);
			return services;
        }
    }
}

