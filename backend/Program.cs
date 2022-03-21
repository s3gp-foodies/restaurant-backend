using foodies_app.Data;
using foodies_app.Data.Repositories;
using foodies_app.Extensions;
using foodies_app.Interfaces;
using foodies_app.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services UNDER this line
//Extension to put all custom services in. Only add services to program.cs if they are not made by us.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services ABOVE this line
var app = builder.Build();

//Configure app UNDER this line
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Websocket config
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets(webSocketOptions);
app.UseWebSockets();

//Request config
app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();

//Add different endpoints
app.MapControllers();
// app.MapHub<PresenceHub>("hubs/presence"); - this is for adding websocket hubs

app.Run();