using Microsoft.EntityFrameworkCore;
using TiendaA01.Data;
using TiendaA01.Services;
using NLog;
using TiendaA01.CrossCuting.Logging;
using Polly;
using Polly.Extensions.Http;
using Azure.Storage.Blobs;

namespace TiendaA01
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);
            
            string path = Directory.GetCurrentDirectory();

            LogManager.Setup().LoadConfigurationFromFile(string.Concat(path, "\\CrossCuting\\Logging\\_nlog.config"));
            builder.Services.AddDbContext<TiendaA01Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaA01Context")
                    .Replace("[DataDirectory]", path)));


            static async Task ProcessAsync() 
            {
                string containerName = "tienda";

                string carpetaLocalRuta = "CrossCuting/Logging";

                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=tiendadeordenadores;AccountKey=dtQRU3ETOWed8GMe79VtBFYe6bRsM0O9JGXRkNzVnew0AD9ZjZmfBm4SMZvba/QVKwDdHjiA96Tu+ASt2Antnw==;EndpointSuffix=core.windows.net";

                BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                Console.WriteLine("Subiendo archivos a Azure Blob Storage...");

                string[] files = Directory.GetFiles(carpetaLocalRuta);

                foreach(string filePath in files)
                {
                    string blobName = "Logs/" + Path.GetFileName(filePath);
                }

            }
            // Add services to the container.
            builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IRepositorioComponente, APIComponenteRepositorio>();
            builder.Services.AddSingleton<IRepositorioOrdenador, APIOrdenadorRepositorio>();
            builder.Services.AddSingleton<IRepositorioPedido, APIPedidoRepositorio>();


            builder.Services.AddHttpClient("MyHttpClient")
                .AddPolicyHandler(GetCircuitBreakerPolicy());


            var app = builder.Build();

 /*           using (var scope= app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                TiendaA01DbInitializer.Initializer(services);
            }*/

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                // pattern: "{controller=Componentes}/{action=Index}/{id?}");
                pattern: "{controller=Home}/{action=Index}");
            app.Run();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3, 
                    durationOfBreak: TimeSpan.FromSeconds(20)
                );
        }
    }
}