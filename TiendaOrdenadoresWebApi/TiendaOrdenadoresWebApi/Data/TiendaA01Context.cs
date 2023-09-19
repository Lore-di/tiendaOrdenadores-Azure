using TiendaOrdenadoresWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaOrdenadoresWebApi.Data

{
    public class TiendaA01Context : DbContext
    {
        public TiendaA01Context(DbContextOptions<TiendaA01Context> options)
            : base(options)
        {

        }

        public DbSet<Componente> Componente { get; set; } = default!;
        public DbSet<Ordenador> Ordenador { get; set; } = default!;
        public DbSet<Pedido> Pedido { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Componente>()
                .HasOne(c => c.Ordenador)
                .WithMany(o => o.Componentes)
                .HasForeignKey(c => c.OrdenadorId);

            modelBuilder.Entity<Ordenador>()
                .HasOne(o => o.Pedido)
                .WithMany(p => p.Ordenadores)
                .HasForeignKey(o => o.PedidoId);
        }
    }
}
