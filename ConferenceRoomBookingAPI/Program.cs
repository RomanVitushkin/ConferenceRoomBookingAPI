using ConferenceRoomBookingAPI.Data; // Простір імен для контексту бази даних
using Microsoft.EntityFrameworkCore; // Простір імен для EF Core та DbContext

var builder = WebApplication.CreateBuilder(args);

// Додаємо рядок підключення до бази даних з appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Реєструємо контекст бази даних у DI контейнері
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
