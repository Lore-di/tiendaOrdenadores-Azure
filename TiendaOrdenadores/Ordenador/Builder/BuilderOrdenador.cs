using TiendaOrdenadoresA.Componentes;
using TiendaOrdenadoresA.Componentes.Builder;
using TiendaOrdenadoresA.Ordenador.Validador;

namespace TiendaOrdenadoresA.Ordenador.Builder
{
    public class BuilderOrdenador : IBuilderOrdenador
    {
        public Componente? DameOrdenador(EnumOrdenadoresTipo tipo)
        {
            Componente? miOrdenador = null;
            ValidadorOrdenadorAttribute validador = new ();
            switch (tipo)
            {
                case EnumOrdenadoresTipo.OrdenadorMaria:
                    BuilderComponente builder = new();
                    IComponente? procesador = builder.DameComponente(EnumComponente.ProcesadorInteli7789Xcs);
                    IComponente? ram = builder.DameComponente(EnumComponente.BancoDeMemoriaSdram879Fh);
                    IComponente? disco = builder.DameComponente(EnumComponente.DiscoDuroSanDisk789Xx);
                    if (procesador != null && ram != null && disco != null)
                        miOrdenador = new Componente(procesador, ram, disco);
                    break;
            }
            if (miOrdenador is not null)
            {
                if (validador.IsValid(miOrdenador))
                    return miOrdenador;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public Componente? DameOrdenador(IComponente procesador, IComponente ram, IComponente disco)
        {
            Componente miComponente = new(procesador, ram, disco);
            ValidadorOrdenadorAttribute validador = new();
            if (validador.IsValid(miComponente)) 
                return miComponente;
            else 
                return null;
        }
    }
}
