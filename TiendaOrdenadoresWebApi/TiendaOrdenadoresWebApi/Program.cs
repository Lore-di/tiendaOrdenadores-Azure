using Microsoft.EntityFrameworkCore;
using NLog;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using TiendaOrdenadoresWebApi.CrossCuting.Logging;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Services;

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

        // Add services to the container.


        //ADO

        builder.Services.AddScoped<ADOContext>(c =>
        {
            var connectionString = builder.Configuration.GetConnectionString("TiendaA01Context") ?? "";
            var dataDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));
            connectionString = connectionString.Replace("[DataDirectory]", dataDirectory);

            return new ADOContext(connectionString);
        });


        builder.Services.AddControllers();
        
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = null;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSwagger",
                builder => builder.WithOrigins("https://localhost", "http://localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        builder.Services.AddTransient<SqlConnection>(p =>
            new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IRepositorioComponente, ADORepositorioComponente>();
        builder.Services.AddScoped<IRepositorioOrdenador, RepositorioOrdenador>();
        builder.Services.AddScoped<IRepositorioPedido, RepositorioPedido>();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
