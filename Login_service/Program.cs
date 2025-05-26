using Login_service.Database;
using Login_service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var credentials = builder.Configuration.GetConnectionString("credentials");

builder.Services.AddDbContext<UsersDb>(db => db.UseMySql(credentials, new MySqlServerVersion(ServerVersion.AutoDetect(credentials))));
builder.Services.AddScoped<UserServices>();

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
