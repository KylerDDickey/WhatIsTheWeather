using DataControllers.Abstract;
using DataControllers.Remote;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhatIsTheWeather
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped(sp => new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
                });

            builder.Services
                .AddSingleton<IOpenWeatherClient, OpenWeatherClient>();

            builder.Services
                .AddHttpClient<IOpenWeatherClient, OpenWeatherClient>(client =>
                {
                    client.BaseAddress = new Uri($"https://api.openweathermap.ord/data/2.5");
                })
                .AddTransientHttpErrorPolicy(policyBuilder => 
                {
                    return policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5));
                });

            await builder
                .Build()
                .RunAsync();
        }
    }
}
