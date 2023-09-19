using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Componentes.Builder
{
    public interface IBuilderComponente
    {
        Componente? DameComponente(EnumComponente tipo);
        Componente? DameComponente(string serie, string descripcion, int calor, long megas, int cores, decimal coste, EnumTipoComponente tipo);

    }
}
