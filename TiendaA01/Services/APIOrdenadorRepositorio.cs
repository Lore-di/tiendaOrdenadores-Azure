using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TiendaA01.Models;

namespace TiendaA01.Services
{
    public class APIOrdenadorRepositorio : IRepositorioOrdenador
    {
        private readonly HttpClient _httpClient;
        private const string urlBase = "https://tiendaordenadoresproyecto-prueba.azurewebsites.net/api/Ordenadores";

        public APIOrdenadorRepositorio(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Cliente");
        }
        public void AddOrdenador(Ordenador ordenador)
        {
            var myContent = JsonConvert.SerializeObject(ordenador);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = _httpClient.PostAsync(urlBase, byteContent).Result;
        }

        public void BorraOrdenador(int id)
        {
            var callResponse = _httpClient.DeleteAsync($"{urlBase}/{id}").Result;
        }

        public float DamePrecio(int id)
        {
            var callResponse = _httpClient.GetAsync($"{urlBase}/{id}").Result;

            if (callResponse.IsSuccessStatusCode)
            {
                var response = callResponse.Content.ReadAsStringAsync().Result;
                var ordenador = JsonConvert.DeserializeObject<Ordenador>(response);

                if (ordenador != null && ordenador.Componentes != null)
                {
                    // Calcular el precio sumando los costes de los componentes
                    float precio = ordenador.Componentes.Sum(c => c.Coste);
                    return precio;
                }
            }

            throw new Exception($"No se pudo obtener el precio del ordenador con ID {id}");

        }

        public List<Ordenador> ListaOrdenadores()
        {
            var callResponse = _httpClient.GetAsync(urlBase).Result;

            var response = callResponse.Content.ReadAsStringAsync().Result;
            var lista = JsonConvert.DeserializeObject<List<Ordenador>>(response);
            if (lista == null)
            {
                return new List<Ordenador>();
            }

            return lista;
        }

        public Ordenador? TomaOrdenador(int id)
        {
            var callResponse = _httpClient.GetAsync($"{urlBase}/{id}").Result;
            var response = callResponse.Content.ReadAsStringAsync().Result;
            var ordenador = JsonConvert.DeserializeObject<Ordenador>(response);
            if (ordenador == null)
            {
                return new Ordenador();
            }

            return ordenador;
        }

        public void UpdateOrdenador(Ordenador ordenador)
        {
            string ordenadorJson = JsonConvert.SerializeObject(ordenador);
            var content = new StringContent(ordenadorJson, Encoding.UTF8, "application/json");
            var callResponse = _httpClient.PutAsync($"{urlBase}/{ordenador.Id}", content).Result;
        }
    }
}
