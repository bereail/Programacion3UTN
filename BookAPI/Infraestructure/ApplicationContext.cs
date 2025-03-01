using Domain.Entities.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infraestructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasDiscriminator<UserRole>("Role") // Aquí usar UserRole en lugar de int
                .HasValue<Admin>(UserRole.Admin)
                .HasValue<Client>(UserRole.Client);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasColumnName("Role")
                .HasConversion<int>();

            // Configuración de datos iniciales
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Name = "Admin User",
                    Email = "admin@example.com",
                    Password = "securepassword",
                    UserName = "admin",
                    Role = UserRole.Admin, // Se almacena como 1 en la BD
                    IsActive = true
                }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 2,
                    Name = "Client User",
                    Email = "client@example.com",
                    Password = "securepassword",
                    UserName = "client",
                    Role = UserRole.Client, // Se almacena como 2 en la BD
                    IsActive = true
                }
            );

            base.OnModelCreating(modelBuilder);
        }


    }
}