using TiendaA01.Models;

namespace TiendaA01.Services
{
    public interface IRepositorioOrdenador
    {
        Ordenador? TomaOrdenador(int id);
        void BorraOrdenador(int id);
        void AddOrdenador(Ordenador ordenador);
        List<Ordenador> ListaOrdenadores();
        void UpdateOrdenador(Ordenador ordenador);

        float DamePrecio(int id);
    }
}
