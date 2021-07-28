using System;
using Microsoft.EntityFrameworkCore;
using RightPlaceBL.Model;

namespace RightPlaceBL.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Chat> Chats { get; set; }
        public ApplicationContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=WIN-1TE5KD8FSVD\MSSQLSERVER01;Database=userappdb;Trusted_Connection=True;");
        }
    }
}
