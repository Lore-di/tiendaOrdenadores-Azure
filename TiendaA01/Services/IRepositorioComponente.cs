using TiendaA01.Models;

namespace TiendaA01.Services
{
    public interface IRepositorioComponente
    {
        Componente? TomaComponente(int id);
        void BorraComponente(int id);
        void AddComponente(Componente componente);
        List<Componente> ListaComponentes();
        void UpdateComponente(Componente componente);
    }
}
