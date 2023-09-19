using TiendaOrdenadoresA.Componentes;

namespace TiendaOrdenadoresA.Ordenador.Builder
{
    public interface IBuilderOrdenador
    {

        Componente? DameOrdenador(EnumOrdenadoresTipo tipo);

        public Componente? DameOrdenador(IComponente procesador, IComponente ram, IComponente disco);

    }
}

