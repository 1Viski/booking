using Booking.App;
using Booking.Core.Abstracts;
using Booking.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<App>();
        services.AddScoped<IDataService, DataService>();
        services.AddScoped<IQueryService, QueryService>();
    })
    .Build();

builder
    .Services
    .GetRequiredService<App>()
    .Run(args);
