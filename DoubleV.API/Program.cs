using DoubleV.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// DI Infra (DbContext + lo que tengas ahí)
builder.Services.AddExternal(builder.Configuration);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

// Si luego usas JWT/cookies, agrega también AddAuthentication(...) aquí
// builder.Services.AddAuthentication("Bearer").AddJwtBearer(...);

// Si luego configuras auth real, descomenta esto también:
// app.UseAuthentication();

// MediatR (ajusta el typeof(...) a cualquier clase de tu capa Application)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(DoubleV.Application.Command.Login.LoginCommand).Assembly)
);

// CORS (para Angular)
var allowedOrigin = builder.Configuration["AllowedOrigins"] ?? "http://localhost:4200";
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CorsPolicy");

// app.UseAuthorization(); // solo si luego agregas auth
app.MapControllers();

app.Run();

