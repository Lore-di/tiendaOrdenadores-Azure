using Microsoft.EntityFrameworkCore;
using TiendaA01.Models;

namespace TiendaA01.Data
{
    public class TiendaA01Context : DbContext
    {
        public TiendaA01Context (DbContextOptions<TiendaA01Context> options)
            : base(options)
        {
            
        }

        public DbSet<TiendaA01.Models.Componente> Componente { get; set; } = default!;
        public DbSet<TiendaA01.Models.Ordenador> Ordenador { get; set; } = default!;
        public DbSet<TiendaA01.Models.Pedido> Pedido { get; set; } = default!;

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
