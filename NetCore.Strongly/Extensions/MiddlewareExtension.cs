using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.Strongly.Extensions
{
    public static class MiddlewareExtension
    {

        public static IApplicationBuilder UseStrongly(this IApplicationBuilder app) 
            => app.UseMiddleware<StronglyMiddleware>();

    }
}
