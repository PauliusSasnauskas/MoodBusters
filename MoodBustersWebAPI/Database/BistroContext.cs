using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MoodBustersWebAPI.Database
{
    public class BistroContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = WebConfigurationManager.ConnectionStrings["MoodBistroDBConnectionString"].ConnectionString;
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogRecord>().ToTable("LogRecord");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasIndex(a => a.Ip).IsUnique();
            modelBuilder.Entity<LogRecord>().Property(record => record.DateTime).HasDefaultValue(DateTime.Now);
        }

        public DbSet<LogRecord> LogRecords { get; set; }
        public DbSet<User> Users { get; set; }
    }
}