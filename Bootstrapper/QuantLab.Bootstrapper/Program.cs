using NLog;
using NLog.Web;
using QuantLab.Bootstrapper;
using QuantLab.Shared.Infrastructure.Modules;
try
{
    var logger = LogManager
        .Setup()
        .LoadConfigurationFromFile("nlog.config")
        .GetCurrentClassLogger();

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var services = builder.Services;

    builder.Logging.ClearProviders();

    builder.Host.UseNLog();
    IConfiguration configuration = builder.Configuration;
    builder.Host.ConfigureModules();
    var modules = services.RegisterModules(configuration);
    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();



    var app = builder.Build();

    // Configure the HTTP request pipeline.
   
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapOpenApi();
    


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();



    app.Run();

}
catch (Exception e)
{
    // En cas d'exception critique avant le d�marrage de l'app
    var logger = LogManager.GetCurrentClassLogger();
    logger.Error(e, "Application stoppée suite à une exception");

    throw;

}
finally
{
    LogManager.Shutdown();
}