using System.Windows.Markup;
using Microsoft.Data.SqlClient;
using TiendaOrdenadoresWebApi.CrossCuting.Logging;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TiendaOrdenadoresWebApi.Services
{
    public class ADORepositorioComponente : IRepositorioComponente
    {
        private readonly ADOContext _context;
        private readonly ILoggerManager _loggerManager;

        public ADORepositorioComponente(ADOContext context, ILoggerManager loggerManager)
        {
            _context = context;
            _loggerManager = loggerManager;
        }
        public void AddComponente(Componente componente)
        {
            var conexion = _context.GetConnection();
            conexion.Open();


            string sql = "INSERT INTO Componente (Calor, Descripcion, Coste, Megas, Cores, Serie, TipoComponente, OrdenadorId) VALUES (@Calor, @Descripcion, @Coste, @Megas, @Cores, @Serie, @TipoComponente, @OrdenadorId)";

            SqlCommand command = new SqlCommand(sql, conexion);
            command.Parameters.AddWithValue("@Calor", componente.Calor);
            command.Parameters.AddWithValue("@Descripcion", componente.Descripcion);
            command.Parameters.AddWithValue("@Coste", componente.Coste);
            command.Parameters.AddWithValue("@Megas", componente.Megas);
            command.Parameters.AddWithValue("@Cores", componente.Cores);
            command.Parameters.AddWithValue("@Serie", componente.Serie);
            command.Parameters.AddWithValue("@TipoComponente", componente.TipoComponente);
            command.Parameters.AddWithValue("@OrdenadorId", componente.OrdenadorId);


            command.ExecuteNonQuery();
            conexion.Close();

        }

        public void BorraComponente(int id)
        {
            var conexion = _context.GetConnection();
            string sql = "DELETE FROM Componente WHERE Id = @Id";

            SqlCommand command = new SqlCommand(sql, conexion);
            conexion.Open();

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
            conexion.Close();
        }

        public List<Componente> ListaComponentes()
        {
            var conexion = _context.GetConnection();
            var componentes = new List<Componente>();
            
            string sql =
                "SELECT Componente.Id, Componente.Calor, Componente.Descripcion, Componente.Coste, Componente.Megas, Componente.Cores, Componente.Serie, Componente.TipoComponente, Componente.OrdenadorId, Ordenador.Id AS IdOrdenador," +
                "Ordenador.Descripcion AS DescripcionOrdenador, Ordenador.PedidoId" +
                " FROM     Componente INNER JOIN Ordenador ON Componente.OrdenadorId = Ordenador.Id";

            SqlCommand command = new SqlCommand(sql, conexion);
            conexion.Open();

            
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                
                while (dataReader.Read())
                {
                    int componenteId = Convert.ToInt32(dataReader["Id"]);
                    float componenteCoste = (float)Convert.ToDecimal(dataReader["Coste"]);
                    int componenteCalor = Convert.ToInt16(dataReader["Calor"]);
                    string componenteDescripcion = Convert.ToString(dataReader["Descripcion"]);
                    long componenteMegas = (long)Convert.ToInt64(dataReader["Megas"]);
                    int componentesCores = Convert.ToInt32(dataReader["Cores"]);
                    string componentesSerie = Convert.ToString(dataReader["Serie"]);
                    int componentesTipo = Convert.ToInt32(dataReader["TipoComponente"]);
                    int ordenadorId = Convert.ToInt32(dataReader["OrdenadorId"]);
                    Ordenador ordenador = new Ordenador()
                    {
                        Id = Convert.ToInt32(dataReader["IdOrdenador"]),
                        Descripcion = Convert.ToString(dataReader["DescripcionOrdenador"]),
                        Componentes = new List<Componente>(),
                        
                    };


                    componentes.Add(new Componente()
                        { 
                            Id = componenteId, 
                            Serie = componentesSerie,
                            Coste = componenteCoste,
                            OrdenadorId = ordenadorId,
                            Descripcion = componenteDescripcion,
                            TipoComponente = componentesTipo,
                            Megas = componenteMegas,
                            Cores = componentesCores,
                            Calor = componenteCalor,
                            Ordenador = ordenador
                        });
                }

            }

            conexion.Close();
            return componentes;
        }

        public Componente? TomaComponente(int id)
        {
            var conexion = _context.GetConnection();
            var componente = new Componente();
            string sql =
                "Select com.Id, com.Calor, com.Descripcion, com.Coste, com.Megas, com.Cores, com.Serie, com.TipoComponente, com.OrdenadorId, o.Id AS ordId, o.Descripcion AS orD From Componente AS com JOIN Ordenador AS o ON com.OrdenadorId = o.Id" +
                " WHERE com.Id =@id";

            SqlCommand command = new SqlCommand(sql, conexion);
                command.Parameters.AddWithValue("@id", id);
            conexion.Open();

            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int componenteId = Convert.ToInt32(dataReader["Id"]);
                    float componenteCoste = (float)Convert.ToDecimal(dataReader["Coste"]);
                    int componenteCalor = Convert.ToInt16(dataReader["Calor"]);
                    string componenteDescripcion = Convert.ToString(dataReader["Descripcion"]);
                    long componenteMegas = (long)Convert.ToInt64(dataReader["Megas"]);
                    int componentesCores = Convert.ToInt32(dataReader["Cores"]);
                    string componentesSerie = Convert.ToString(dataReader["Serie"]);
                    int componentesTipo = Convert.ToInt32(dataReader["TipoComponente"]);
                    int ordenadorId = dataReader["OrdenadorId"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["OrdenadorId"])
                        : 0;

                    Ordenador ordenador = new Ordenador()
                    {
                        Id = Convert.ToInt32(dataReader["ordId"]),
                        Descripcion = Convert.ToString(dataReader["orD"]),
                    };

                componente.Id = componenteId;
                componente.Serie = componentesSerie;
                componente.Coste = componenteCoste;
                componente.OrdenadorId = ordenadorId;
                componente.Descripcion = componenteDescripcion;
                componente.TipoComponente = componentesTipo;
                componente.Ordenador = ordenador;
                componente.Calor = componenteCalor;
                componente.Cores = componentesCores;
                componente.Megas = componenteMegas;
                };
            }
            conexion.Close();
            return componente;
        }

        public void UpdateComponente(Componente componente)
        {
            var conexion = _context.GetConnection();
            string sql = "UPDATE Componente SET Calor = @Calor, Descripcion = @Descripcion, Coste = @Coste, Megas = @Megas, Cores = @Cores, Serie = @Serie, TipoComponente = @TipoComponente, OrdenadorId = @OrdenadorId WHERE Id = @Id";

            SqlCommand command = new SqlCommand(sql, conexion);
            conexion.Open();

            command.Parameters.AddWithValue("@Calor", componente.Calor);
            command.Parameters.AddWithValue("@Descripcion", componente.Descripcion);
            command.Parameters.AddWithValue("@Coste", componente.Coste);
            command.Parameters.AddWithValue("@Megas", componente.Megas);
            command.Parameters.AddWithValue("@Cores", componente.Cores);
            command.Parameters.AddWithValue("@Serie", componente.Serie);
            command.Parameters.AddWithValue("@TipoComponente", componente.TipoComponente);
            command.Parameters.AddWithValue("@OrdenadorId", componente.OrdenadorId);
            command.Parameters.AddWithValue("@Id", componente.Id);

            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
