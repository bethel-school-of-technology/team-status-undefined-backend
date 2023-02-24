using team_status_undefined_backend.Migrations;
using team_status_undefined_backend.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<BarberDbContext>("Data Source=team_status_undefined_backend.db");
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
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