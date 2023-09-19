using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public interface IRepositorioPedido
    {
        Pedido? TomaPedido(int id);
        void BorraPedido(int id);
        void AddPedido(Pedido pedido);
        List<Pedido> ListaPedido();
        void UpdatePedido(Pedido pedido);
        float DamePrecio(int id);
    }
}
