using System.ComponentModel.DataAnnotations;
using TiendaOrdenadoresA.Componentes.Validador;
using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Componentes.Builder
{
    public class BuilderComponente : IBuilderComponente
    {
        public Componente? DameComponente(EnumComponente tipo)
        {
            return tipo switch
            {
                EnumComponente.ProcesadorInteli7789Xcs => DameComponente("789_XCS", "Procesador Intel i7", 10, 0, 9,
                    134, EnumTipoComponente.Procesador),
                EnumComponente.ProcesadorInteli7789Xcd => DameComponente("789_XCD", "Procesador Intel i7", 12, 0, 10,
                    138, EnumTipoComponente.Procesador),
                EnumComponente.ProcesadorRyzenAMD => DameComponente("797-X3", "Procesador Ryzen", 60, 0, 34, 278, 
                    EnumTipoComponente.Procesador),
                EnumComponente.ProcesadorInteli7789Xct => DameComponente("789_XCT", "Procesador Intel i7", 22, 0, 11,
                    138, EnumTipoComponente.Procesador),
                EnumComponente.BancoDeMemoriaSdram879Fh => DameComponente("879FH", "Banco de memoria SDRAM", 10, 512,
                    0, 100, EnumTipoComponente.MemoriaRam),
                EnumComponente.BancoDeMemoriaSdram879FhL => DameComponente("879FH_L", "Banco de memoria SDRAM", 15,
                    1024, 0, 125, EnumTipoComponente.MemoriaRam),
                EnumComponente.BancoDeMemoriaSdram879FhT => DameComponente("879FH_T", "Banco de memoria SDRAM", 24,
                    2028, 0, 150, EnumTipoComponente.MemoriaRam),
                EnumComponente.DiscoDuroSanDisk789Xx => DameComponente("789_XX", "Disco Duro Scan Disk", 10, 500000,
                    0, 50, EnumTipoComponente.Almacenamiento),
                EnumComponente.DiscoDuroSanDisk789Xx2 => DameComponente("789_XX_2", "Disco Duro Scan Disk", 29,
                    1000000, 0, 90, EnumTipoComponente.Almacenamiento),
                EnumComponente.DiscoDuroSanDisk789Xx3 => DameComponente("789_XX_3", "Disco Duro Scan Disk", 39,
                    2000000, 0, 128, EnumTipoComponente.Almacenamiento),
                _ => null
            };
        }

        public Componente? DameComponente(string serie, string descripcion, int calor, long megas, int cores, decimal coste, EnumTipoComponente tipo)
        {
            ValidationAttribute validador = new ValidadorComponenteAttribute();
            ISerie miSerie;
            if (serie != "")
                miSerie = new ConSerie(serie);
            else
                miSerie = new SinSerie();

            IDescripcion miDescripcion;
            if (descripcion != "")
                miDescripcion = new ConDescripcion(descripcion);
            else
                miDescripcion = new SinDescripcion();

            ICalor miCalor;
            if (calor == 0)
                miCalor = new SinCalor();
            else
                miCalor = new ConCalor(calor);

            IMegas miMegas;
            if (megas == 0)
                miMegas = new SinMegas();
            else
                miMegas = new ConMegas(megas);


            ICores miCores;
            if (cores == 0)
                miCores = new SinCores();
            else
                miCores = new ConCores(cores);

            IPrecio miPrecio;
            if (coste == 0)
                miPrecio = new SinPrecio();
            else
                miPrecio = new ConPrecio(coste);

            Componente miComponente = new(miSerie, miDescripcion, miCalor, miMegas, miCores, miPrecio, tipo);
            if (validador.IsValid(miComponente))
                return miComponente;
            else
                return null;
        }
    }
}
