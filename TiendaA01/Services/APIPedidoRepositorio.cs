using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TiendaA01.Models;

namespace TiendaA01.Services
{
    public class APIPedidoRepositorio : IRepositorioPedido
    {
        
        private readonly HttpClient _httpClient;
        private const string urlBase = "https://tiendaordenadoresproyecto-prueba.azurewebsites.net/api/Pedidos";

        public APIPedidoRepositorio(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Cliente");
        }

        public void AddPedido(Pedido pedido)
        {
            var myContent = JsonConvert.SerializeObject(pedido);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = _httpClient.PostAsync(urlBase, byteContent).Result;
        }

        public void BorraPedido(int id)
        {
            var callResponse = _httpClient.DeleteAsync($"{urlBase}/{id}").Result;
        }

        public float DamePrecio(int id)
        {
            var callResponse = _httpClient.GetAsync($"{urlBase}/{id}").Result;

            if (callResponse.IsSuccessStatusCode)
            {
                var response = callResponse.Content.ReadAsStringAsync().Result;
                var pedido = JsonConvert.DeserializeObject<Pedido>(response);

                if (pedido != null && pedido.Ordenadores != null)
                {
                    float precioTotal = pedido.Ordenadores.Sum(o => o.Componentes.Sum(c => c.Coste));
                    return precioTotal;
                }
            }

            
            throw new Exception($"No se pudo obtener el precio del pedido con ID {id}");

        }

        public List<Pedido> ListaPedido()
        {
            var callResponse = _httpClient.GetAsync(urlBase).Result;

            var response = callResponse.Content.ReadAsStringAsync().Result;
            var lista = JsonConvert.DeserializeObject<List<Pedido>>(response);
            if (lista == null)
            {
                return new List<Pedido>();
            }

            return lista;
        }

        public Pedido? TomaPedido(int id)
        {
            var callResponse = _httpClient.GetAsync($"{urlBase}/{id}").Result;
            var response = callResponse.Content.ReadAsStringAsync().Result;
            var pedido = JsonConvert.DeserializeObject<Pedido>(response);
            if (pedido == null)
            {
                return new Pedido();
            }

            return pedido;
        }

        public void UpdatePedido(Pedido pedido)
        {
            string pedidoJson = JsonConvert.SerializeObject(pedido);
            var content = new StringContent(pedidoJson, Encoding.UTF8, "application/json");
            var callResponse = _httpClient.PutAsync($"{urlBase}/{pedido.Id}", content).Result;
        }
    }
}
