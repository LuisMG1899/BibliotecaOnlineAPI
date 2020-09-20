using BibliotecaOnlineAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Infraestructure
{
    public class BibliotecaDBContext : DbContext
    {
        public BibliotecaDBContext(DbContextOptions<BibliotecaDBContext> options) : base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JI0ALMN;Database=BibliotecaDB;integrated security=True;");
        }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libros>().ToTable("Libros");
            modelBuilder.Entity<Libros>()
                .HasOne<Usuarios>(x => x.UsuarioLibro)
                .WithMany(x => x.LibrosUsuario)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<Libros>().HasKey(x => x.Id);
            modelBuilder.Entity<Libros>().Property(x => x.Nombre).HasColumnName("Nombre").IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Libros>().Property(x => x.Activo).HasColumnName("Activo").IsRequired();
            modelBuilder.Entity<Libros>().Property(x => x.FechaCreacion).HasColumnName("FechaCreacion").IsRequired();

            //modelBuilder.Entity<Usuarios>().HasMany<Libros>(x => x.LibrosUsuario).WithOne(x => x.UsuarioLibro).HasForeignKey(x => x.);

            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Usuarios>().HasKey(x => x.IdUsuario);
            
        }



    }
}
