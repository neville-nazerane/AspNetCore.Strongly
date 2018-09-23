using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.Strongly.Extensions
{
    public static class MiddlewareExtension
    {

        public static void UseStrongly(this IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
                var found = context.RequestServices
                            .GetService<PathMappings>()[context.Request.Path];
                if (found == null) await next();
                else
                {
                    found(context);
                }
            });
        }

    }
}
