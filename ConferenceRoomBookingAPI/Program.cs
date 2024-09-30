using ConferenceRoomBookingAPI.Data; // ������ ���� ��� ��������� ���� �����
using Microsoft.EntityFrameworkCore; // ������ ���� ��� EF Core �� DbContext

var builder = WebApplication.CreateBuilder(args);

// ������ ����� ���������� �� ���� ����� � appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// �������� �������� ���� ����� � DI ���������
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
