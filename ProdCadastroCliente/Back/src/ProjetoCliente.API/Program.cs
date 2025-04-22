using Microsoft.EntityFrameworkCore;
using ProjetoCliente.Application;
using ProjetoCliente.Application.Contratos;
using ProjetoCliente.Persistence;
using ProjetoCliente.Persistence.Contextos;
using ProjetoCliente.Persistence.Contratos;
using System.Text.Json.Serialization;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Configurações do app
var configuration = builder.Configuration;

 Batteries.Init(); // Garante a inicialização do SQLite

// 🔌 Conexão com banco
builder.Services.AddDbContext<ProjetoClientesContext>(
    context => context.UseSqlite(configuration.GetConnectionString("Default"))
);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// 📦 AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 🧩 Injeção de dependência
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<IClientePersist, ClientePersist>();

// ✅ Swagger
builder.Services.AddSwaggerGen(); // <- ESSA LINHA RESOLVE O ERRO

// 🧾 Controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 🚀 Build do app
var app = builder.Build();

// 🔧 Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoCliente.API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();

