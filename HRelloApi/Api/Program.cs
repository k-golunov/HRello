using System.Text;
using Dal;
using Dal.Email;
using Dal.Email.Interfaces;
using Dal.Entities;
using Dal.User.Repositories;
using HRelloApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using AutoMapper;
using Dal.Tasks.Repositories;
using Dal.Tasks.Repositories.Interfaces;
using Dal.User.Repositories.Interfaces;
using HRelloApi.Controllers.Public.Auth.Mapping;
using HRelloApi.Controllers.Public.Departament.Mapping;
using Logic.Managers.Departament;
using Logic.Managers.Departament.Interfaces;
using Logic.Managers.Task;
using Logic.Managers.Task.Interfaces;
using Microsoft.IdentityModel.Tokens;
using RollbarDotNet.Payloads;

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

// Настройки аутентификации через JwtBearer
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecretKey"]))
        };
    });

// подключение к бд
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// добавление айдентити, тестовая
// надо усложнить требования к паролю
builder.Services.AddIdentity<UserDal, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// конфигурация айдентити 
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<UserDal>()
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddDeveloperSigningCredential();

// TODO delete?
// builder.Services.ConfigureApplicationCookie(config =>
// {
//     config.Cookie.Name = "Notes.Identity.Cookie";
//     config.LoginPath = "/Auth/Login";
//     config.LogoutPath = "/Auth/Logout";
// });
// Add services to the container.

builder.Services.AddControllers();

// Тестовые репозиторий для бд почты. Требует удаления
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
// Репозиторий пользователя
builder.Services.AddScoped<UserRepository>();
// Мененджер пользователя
builder.Services.AddScoped<UserManager<UserDal>>();
// ???
//builder.Services.AddScoped(typeof(Logic.Managers.UserManager<>));
// Мэненджер ролей из идентити
builder.Services.AddScoped<RoleManager<IdentityRole>>();
// работа с отдеклами
builder.Services.AddScoped<IDepartamentManager, DepartamentManager>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
//работа с задачами
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskStatusManager, StatusManager>();
builder.Services.AddScoped<ITaskManager, TaskManager>();
// Маппинг 
builder.Services.AddAutoMapper(typeof(AccountMappingProfile));
builder.Services.AddAutoMapper(typeof(CreateUserMappingProfile));
builder.Services.AddAutoMapper(typeof(DepartamentProfiles));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "Api.xml");
    options.IncludeXmlComments(xmlPath);
});

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
/*#if DEBUG


using (var scope = 
       app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<DataContext>())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
#endif  */      
        

app.MapControllers();

app.Run();