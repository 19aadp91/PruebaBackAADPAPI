using AADP.API.ExtensionProgram;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var logger = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.ff zzz} {CorrelationId} {Level:u3}] {Username} {Message:lj}{NewLine}{Exception}")
        .Enrich.FromLogContext()
        .CreateLogger();
    builder.Host.UseSerilog(logger);

    builder.Services.ConfiguringControllers();
    builder.Services.ConfiguringCors();
    builder.Services.ConfigureAuthentication(builder.Configuration);
    builder.Services.AddServices(builder.Configuration);
    builder.Services.ConfiguringSwagger();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.UseCors("CorsPolicy");

    app.Run();
}

catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}