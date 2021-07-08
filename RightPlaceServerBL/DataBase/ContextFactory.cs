using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RightPlaceBL.DataBase
{
    //public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    //{
    //    public ApplicationContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

    //        ConfigurationBuilder builder = new ConfigurationBuilder();
    //        builder.SetBasePath(Directory.GetCurrentDirectory());
    //        builder.AddJsonFile("json1.json");
    //        IConfiguration config = builder.Build();

    //        string connectionString = config.GetConnectionString("DefaultConnection");
    //        optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
    //        return new ApplicationContext(optionsBuilder.Options);
    //    }
    //}
}
