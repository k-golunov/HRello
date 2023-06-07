namespace HRelloApi.ProgramExtension;

public static class HubExtentions
{
    public static IServiceCollection AddHubs(this IServiceCollection services)
    {
        services.AddSignalR();
        
        return services;
    }
}