using Microsoft.AspNetCore.Http;
using NetCore.Strongly.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Strongly.Extensions
{
    class StronglyMiddleware
    {
        private readonly RequestDelegate _Next;

        public StronglyMiddleware(RequestDelegate _next)
        {
            _Next = _next;
        }

        public async Task InvokeAsync(HttpContext context, PathMappings pathMappings)
        {

            if (context.Request.Path == "/stronglyscript.js")
            {
                context.Response.Headers.Add("Content-Type", "text/javascript;charset=UTF-8");
                await context.Response.WriteAsync(JsContent.content);
                return;
            }

            var found = pathMappings[context.Request.Path];
            if (found == null) await _Next(context);
            else
            {
                found(context);
            }

        }


    }
}
