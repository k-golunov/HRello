using System.Text;
using System.Text.Json.Serialization;
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
using HRelloApi.Controllers.Public.Tasks.mapping;
using HRelloApi.Controllers.Public.User.Mapping;
using HRelloApi.ProgramExtension;
using Logic.Exceptions.Base;
using Logic.Managers.Departament;
using Logic.Managers.Departament.Interfaces;
using Logic.Managers.Tasks;
using Logic.Managers.Tasks.Filters.mapping;
using Logic.Managers.Tasks.Interfaces;
using Logic.Managers.Tasks.StatusesTree;
using Microsoft.AspNetCore.Diagnostics;
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

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// работа с отделами
builder.Services.AddScoped<IDepartamentManager, DepartamentManager>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
//работа с задачами
builder.Services.AddTasks();
builder.Services.AddTasksResults();
// Маппинг 
builder.Services.AddAutoMapper(typeof(AccountMappingProfile));
builder.Services.AddAutoMapper(typeof(CreateUserMappingProfile));
builder.Services.AddAutoMapper(typeof(DepartamentProfiles));
builder.Services.AddAutoMapper(typeof(UserMapping));
builder.Services.AddAutoMapper(typeof(UserResultProfile));
builder.Services.AddAutoMapper(typeof(BossResultProfile));
builder.Services.AddAutoMapper(typeof(TaskProfile));
builder.Services.AddAutoMapper(typeof(FiltersProfile));
builder.Services.AddAutoMapper(typeof(FilteredTaskProfile));

builder.Services.AddControllers()
    .AddJsonOptions(opt=> { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHost();

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

app.UseCors();

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
app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature.Error;
    if (exception is BaseException baseException)
    {
        context.Response.StatusCode = baseException.Status;
        await context.Response.WriteAsJsonAsync(new { message = exception.Message, code = baseException.Code});
    }
}));
app.Run();