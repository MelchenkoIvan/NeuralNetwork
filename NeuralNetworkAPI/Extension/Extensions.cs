using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeuralNetworkCore;
using NeuralNetworkCore.Mapper;
using NeuralNetworkDatabase;

namespace NeuralNetworkAPI.Extension
{
	public static class Extensions
	{
		public static IServiceCollection AddCore(this IServiceCollection services) =>
			services.AddScoped<IUserRepositroy, UserRepository>()
				.AddScoped<INeuralNetworkRepository, NeuralNetworkRepository>()
				.AddAutoMapper(typeof(UserProfile));
		

		public static IServiceCollection AddServices(this IServiceCollection services) =>
			services.AddScoped<IUserService, UserService>();

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

