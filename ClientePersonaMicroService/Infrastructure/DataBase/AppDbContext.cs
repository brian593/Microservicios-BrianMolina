using System;
using ClientePersonaMicroService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientePersonaMicroService.Infrastructure.DataBase
{
	

    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

