using Microsoft.AspNetCore.Http;
using NetCore.Strongly.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Strongly.Extensions
{
    class StronglyMiddleware
    {
        private readonly RequestDelegate _Next;

        internal const string startPath = "/netcoreStrongly";
        internal const string scriptPath = "/stronglyscript.js";

        public StronglyMiddleware(RequestDelegate _next)
        {
            _Next = _next;
        }

        public async Task InvokeAsync(HttpContext context, PathHandler pathHandler)
        {

            var path = context.Request.Path;

            if (path == scriptPath)
            {
                context.Response.Headers.Add("Content-Type", "text/javascript;charset=UTF-8");
                await context.Response.WriteAsync(JsContent.allContent);
                return;
            }

            if (path.StartsWithSegments(startPath))
            {
                string body = "";
                using (var stream = new StreamReader(context.Request.Body))
                {
                    body = stream.ReadToEnd();
                }
                
                var result = pathHandler.Execute(path.Value.Substring(startPath.Length + 1), JavaScriptHandler.Create(body));
                if (result)
                {
                    if (result.Data == null)
                        context.Response.StatusCode = 204;
                    else
                    {
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result.Data));
                    }
                }
                else context.Response.StatusCode = 404;
                return;
            }

            await _Next(context);

            //var found = pathMappings[context.Request.Path];
            //if (found == null) await _Next(context);
            //else
            //{
            //    found(context);
            //}

        }


    }
}
