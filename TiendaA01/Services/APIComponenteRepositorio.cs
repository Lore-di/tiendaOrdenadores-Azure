using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TiendaA01.Models;

namespace TiendaA01.Services
{
    public class APIComponenteRepositorio : IRepositorioComponente
    {
        private readonly HttpClient _httpClient;
        private const string urlBase = "https://tiendaordenadoresproyecto-prueba.azurewebsites.net/api/Componentes";
        public APIComponenteRepositorio(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Cliente");
        }


        public void AddComponente(Componente componente)
        {
            /*string componenteJson = JsonConvert.SerializeObject(componente);

            var content = new StringContent(componenteJson, Encoding.UTF8, "application/json");
            var callResponse = _httpClient.PostAsync("http://localhost:5294/api/Componentes", content).Result;*/

            var myContent = JsonConvert.SerializeObject(componente);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = _httpClient.PostAsync(urlBase, byteContent).Result;
        }

        public void BorraComponente(int id)
        {
            var callResponse = _httpClient.DeleteAsync($"{urlBase}/{id}").Result;
        }

        public List<Componente> ListaComponentes()
        {
            var callResponse = _httpClient.GetAsync(urlBase).Result;

            var response = callResponse.Content.ReadAsStringAsync().Result;
            var lista = JsonConvert.DeserializeObject<List<Componente>>(response);
            if (lista == null)
            {
                return new List<Componente>();
            }

            return lista;
        }

        public Componente? TomaComponente(int id)
        {
            var callResponse = _httpClient.GetAsync($"{urlBase}/{id}").Result;
            var response = callResponse.Content.ReadAsStringAsync().Result;
            var componente = JsonConvert.DeserializeObject<Componente>(response);
            if (componente == null)
            {
                return new Componente();
            }

            return componente;
        }

        public void UpdateComponente(Componente componente)
        {
            string componenteJson = JsonConvert.SerializeObject(componente);
            var content = new StringContent(componenteJson, Encoding.UTF8, "application/json");
            var callResponse = _httpClient.PutAsync($"{urlBase}/{componente.Id}", content).Result;

        }
    }
}
