using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Strongly.Services;

namespace NetCore.Strongly.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddStrongly(this IServiceCollection services,
                            Action<StronglyServiceOptions> options = null)
        {
            services
                //.AddSingleton<PathMappings>()
                    .AddSingleton<TypeHandler>()
                    .AddScoped<PathHandler>()
                    .AddScoped<JavaScriptData>()
                    .AddScoped<JavaScriptHandler>()
                                   .AddScoped(typeof(StronglyControl<>));
            var opts = new StronglyServiceOptions(services);
            options?.Invoke(opts);
            //var mappings = new PathMappings();
            //opts.mappingActions.ForEach(act => act(mappings));
            //services.AddSingleton(mappings);
            return services;
        }
    }
}
