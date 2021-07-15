using System;
using Microsoft.EntityFrameworkCore;

namespace RightPlaceBL.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set;}
        public ApplicationContext() 
        {
            Database.EnsureCreated();        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=WIN-1TE5KD8FSVD\MSSQLSERVER01;Database=userappdb;Trusted_Connection=True;");
        }
    }
}
