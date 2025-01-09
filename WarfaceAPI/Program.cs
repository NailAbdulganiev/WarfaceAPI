using Microsoft.EntityFrameworkCore;
using WarfaceAPI.Data;
using WarfaceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем строку подключения к PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем ApiClient с использованием IHttpClientFactory
builder.Services.AddHttpClient();
builder.Services.AddScoped<ApiClient>();

// Регистрация TrackerDataService
builder.Services.AddScoped<TrackerDataService>();

// Регистрация фонового сервиса
builder.Services.AddHostedService<PlayerStatsUpdater>();

// Добавление Razor Pages
builder.Services.AddRazorPages(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

// Настройка маршрутов для Razor Pages
app.MapRazorPages(); 

app.UseHttpsRedirection();
app.MapControllers();
app.Run();