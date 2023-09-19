using Microsoft.EntityFrameworkCore;
using TiendaA01.CrossCuting.Logging;
using TiendaA01.Data;
using TiendaA01.Models;

namespace TiendaA01.Services
{
    public class RepositorioComponente : IRepositorioComponente
    {
        private readonly TiendaA01Context _contexto;
        private readonly ILoggerManager _loggerManager;
       // private readonly IValidadorComponente _validadorComponente;

        public RepositorioComponente(TiendaA01Context dbContext, ILoggerManager loggerManager)
        {
            this._contexto = dbContext;
            _loggerManager = loggerManager;
        }

        public void AddComponente(Componente componente)
        {
            var existeComponente = _contexto.Componente.FirstOrDefault(c => c.Id == componente.Id);

            if (existeComponente == null)
            {
                _contexto.Componente.Add(componente);
                _loggerManager.LogInfo("Componente añadido");
                _contexto.SaveChanges();
            }
        }

        public void BorraComponente(int id)
        {
                var componenteABorrar = TomaComponente(id);
                    _contexto.Componente.Remove(componenteABorrar);
                    _contexto.SaveChanges();
        }
    
        public List<Componente> ListaComponentes()
        {
                return _contexto.Componente.AsNoTracking().Include(p=>p.Ordenador).ToList();
        }

        public Componente TomaComponente(int id)
        {
                var componenteEncontrado = _contexto.Componente.AsNoTracking().Include(p=>p.Ordenador).FirstOrDefault(p=>p.Id == id);
                if (componenteEncontrado != null)
                {
                    _loggerManager.LogInfo("Componente encontrado");
                    return componenteEncontrado;
                }
                else
                {
                    _loggerManager.LogError("Componente no encontrado");
                    return new Componente();
                }
        }

        public void UpdateComponente(Componente componente)
        {
            //var valorAnterior = _contexto.Componente.AsNoTracking().Include(i => i.Ordenador).FirstOrDefault(i=>i.Id == componente.Id);
            _contexto.Update(componente);
            _contexto.SaveChanges();
        }
    }
}
