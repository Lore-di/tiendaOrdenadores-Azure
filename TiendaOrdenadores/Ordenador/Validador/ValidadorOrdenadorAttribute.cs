using System.ComponentModel.DataAnnotations;
using TiendaOrdenadoresA.Componentes.Validador;
using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Ordenador.Validador
{
    public class ValidadorOrdenadorAttribute : ValidationAttribute
    {
        public override bool IsValid(Object? value)
        {
            if (value is Componente ordenador)
            {
                ValidationAttribute validador = new ValidadorComponenteAttribute();
                                
                return ((Componentes.Componente)ordenador.Procesador).TipoComponente ==
                       EnumTipoComponente.Procesador &&
                       ((Componentes.Componente)ordenador.Ram).TipoComponente ==
                       EnumTipoComponente.MemoriaRam &&
                       ((Componentes.Componente)ordenador.Disco).TipoComponente ==
                       EnumTipoComponente.Almacenamiento &&
                       validador.IsValid(ordenador.Procesador as Componentes.Componente) &&
                       validador.IsValid(ordenador.Ram as Componentes.Componente) &&
                       validador.IsValid(ordenador.Disco as Componentes.Componente);

            }
            else
            {
                return false;
            }
        }
    }
}
