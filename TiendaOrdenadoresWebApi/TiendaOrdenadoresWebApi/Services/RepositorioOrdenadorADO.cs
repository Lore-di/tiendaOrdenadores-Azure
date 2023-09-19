using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresWebApi.CrossCuting.Logging;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public class RepositorioOrdenadorADO : IRepositorioOrdenador
    {
        private readonly ADOContext _context;
        private readonly ILoggerManager _loggerManager;

        public RepositorioOrdenadorADO(ADOContext context, ILoggerManager loggerManager)
        {
            _context = context;
            _loggerManager = loggerManager;
        }

        public void AddOrdenador(Ordenador ordenador)
        {
            throw new NotImplementedException();
        }

        public void BorraOrdenador(int id)
        {
            throw new NotImplementedException();
        }

        public float DamePrecio(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ordenador> ListaOrdenadores()
        {
            var conexion = _context.GetConnection();
            var componentes = new List<Componente>();
            string sql = "Select * From Componente";

            SqlCommand command = new SqlCommand(sql, conexion);
            conexion.Open();

            // Obtain a data reader via ExecuteReader().
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    int componenteId = Convert.ToInt32(dataReader["Id"]);
                    string componenteNombre = Convert.ToString(dataReader["Serie"]);
                    float componenteCoste = (float)Convert.ToDecimal(dataReader["Coste"]);
                    int componenteCalor = Convert.ToInt16(dataReader["Calor"]);
                    string componenteDescripcion = Convert.ToString(dataReader["Descripcion"]);
                    long componenteMegas = (long)Convert.ToInt64(dataReader["Megas"]);
                    int componentesCores = Convert.ToInt32(dataReader["Cores"]);
                    string componentesSerie = Convert.ToString(dataReader["Serie"]);
                    int cocmponentesTipo = Convert.ToInt32(dataReader["TipoComponente"]);
                    int ordenadorId = Convert.ToInt32(dataReader["OrdenadorId"]);


                    componentes.Add(new Componente()
                    {
                        Id = componenteId, Serie = componenteNombre, Coste = componenteCoste, OrdenadorId = ordenadorId
                    });
                }
            }
            conexion.Close();
            return new List<Ordenador>();
        }

        public Ordenador? TomaOrdenador(int id)
            {
                throw new NotImplementedException();
            }

        public void UpdateOrdenador(Ordenador ordenador)
        {
                throw new NotImplementedException();
        }
    }
}
