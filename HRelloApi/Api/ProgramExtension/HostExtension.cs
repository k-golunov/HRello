namespace HRelloApi.ProgramExtension;

public static class HostExtension
{
    public static IServiceCollection AddHost(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(/*name: myOrigins,*/
            policy =>
            {
                policy.WithOrigins("http://185.133.40.145:3033",
                        "http://185.133.40.145:7296")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        return services;
    }
}