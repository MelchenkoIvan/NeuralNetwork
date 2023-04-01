using System;
using Microsoft.EntityFrameworkCore;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkDatabase
{
	public class NeuralNetworkEf : DbContext
	{
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost; initial catalog=NeuralNetworkEf;persist security info=True;user id=SA;password=Passw0rd123;TrustServerCertificate=True");
        }
    }
}

