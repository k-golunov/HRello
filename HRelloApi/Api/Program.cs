using System.Text;
using Dal;
using Dal.Entities;
using Dal.User.Repositories;
using HRelloApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Dal.Tasks.Repositories;
using Dal.Tasks.Repositories.Interfaces;
using Dal.User.Repositories.Interfaces;
using HRelloApi.Controllers.Public.Auth.Mapping;
using HRelloApi.Controllers.Public.Departament.Mapping;
using HRelloApi.ProgramExtension;
using Logic.Managers.Departament;
using Logic.Managers.Departament.Interfaces;
using Logic.Managers.Tasks;
using Logic.Managers.Tasks.Interfaces;
using Logic.Managers.Tasks.StatusesTree;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Подключение логгера
builder.Host.UseSerilog((cts, lc) =>
    lc
        .Enrich.WithThreadId()
        .Enrich.FromLogContext()
        // .AuditTo.Sink<SerilogSink>()
        // .Filter.With<SerilogFilter>()
        // .Enrich.With<SerilogEnrich>()
        .WriteTo.Console(
            LogEventLevel.Information,
            outputTemplate:
            "{Timestamp:HH:mm:ss:ms} LEVEL:[{Level}]| THREAD:|{ThreadId}| Source: |{Source}| {Message}{NewLine}{Exception}"));

LogContext.PushProperty("Source", "Program");

// lowercase для рестов
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddIdentitySettings(builder.Configuration);

// подключение к бд
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

// работа с отделами
builder.Services.AddScoped<IDepartamentManager, DepartamentManager>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
//работа с задачами
builder.Services.AddTasks();
// Маппинг 
builder.Services.AddAutoMapper(typeof(AccountMappingProfile));
builder.Services.AddAutoMapper(typeof(CreateUserMappingProfile));
builder.Services.AddAutoMapper(typeof(DepartamentProfiles));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Подключаем авторизацию, аутентификацию и айдентити
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();

// Откючаем (комментируем) если не требуется отчистка бд 
// т.к. все данные из бд будут удаленны
/*using (var scope = 
       app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<DataContext>())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}*/

app.MapControllers();

app.Run();