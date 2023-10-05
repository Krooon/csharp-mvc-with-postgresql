using exercise.api.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.api.Data
{
    public class DataContext : DbContext 
    {
        private string connectionString;
        public DataContext()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
