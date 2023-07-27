using Microsoft.AspNetCore.Diagnostics;
using NLog;
using NLog.Web;
using TicketManagementApplicationAPI.Middleware;
using TicketManagementApplicationAPI.Repositories;
using TicketManagementApplicationAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init TicketManagementApplicationAPI project");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITestService, TestService>();

builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();