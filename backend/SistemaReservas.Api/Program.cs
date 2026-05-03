var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS para o React conseguir conversar com a API
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Registro do MongoDB
builder.Services.AddSingleton<SistemaReservas.Api.Services.RecursosService>();
builder.Services.AddSingleton<SistemaReservas.Api.Services.AgendamentosService>();

var app = builder.Build();

// Habilita o CORS logo após o Build
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();