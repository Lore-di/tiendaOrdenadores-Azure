using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
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
