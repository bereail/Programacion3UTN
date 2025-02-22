using Domain.Enums;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infraestructure
{
    public class ApplicationContext : DbContext
    {
        //Los warnings los podemos obviar porque DbContext se encarga de eso.

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        //constructor del contexto
        //Llamo al constructor de dbContext que es el que acepta las opciones
        //Cuando pongo base:() se llama al constructor de la clase padre con el parametro que le mando entre ()
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        //Seeding: rellenar la base de datos con un conjunto inicial de datos
        //para hacerlo extendemos el metodo onmodelcreating el metodo .hashdata
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")   // Valor por defecto para la clase base
                .HasValue<Admin>("Admin") // Valor para Admin
                .HasValue<Client>("Client"); // Valor para Client

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    UserId = 1,
                    Name = "Berenice",
                    UserName = "bereail",
                    Password = "123",
                    Email = "bere@gmail.com",

                },
                new Client
                {
                    UserId = 2,
                    Name = "Sofia",
                    UserName = "sofi",
                    Password = "123",
                    Email = "sofi@gmail.com",
                });

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    UserId = 3,
                    UserName = "admin",
                    Password = "123",
                    Email = "admin@gmail.com",
                });

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 4,
                    Title = "Harry Potter",
                    Genre = BookGenre.Juvenil,
                    Price = 10000,
                    Author = "",
                    Stock = 3,
                },
                new Book
                {
                    BookId = 5,
                    Title = "El Aleph",
                    Genre =BookGenre.Literatura,
                    Price = 50000,
                    Author = "",
                    Stock = 2,
                },
                new Book
                {
                    BookId = 7,
                    Title = "1984",
                    Genre = BookGenre.Ficción,
                    Price = 30000,
                    Author = "",
                    Stock = 0,
                });

            modelBuilder.Entity<SaleOrder>()
                .HasOne(s => s.Book)
                .WithMany(p => p.SaleOrders)
                .HasForeignKey(s => s.BookId);

            modelBuilder.Entity<SaleOrder>()
                .HasOne(s => s.Client)
                .WithMany(c => c.SaleOrders)
                .HasForeignKey(c => c.ClientId);

            base.OnModelCreating(modelBuilder);
        }
    }
}