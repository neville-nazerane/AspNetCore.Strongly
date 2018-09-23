using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NetCore.Strongly.Services;
using System.IO;

namespace NetCore.Strongly.Extensions
{
    class PathMappings
    {

        readonly Dictionary<string, Action<HttpContext>> contents;

        public PathMappings()
        {
            contents = new Dictionary<string, Action<HttpContext>>();
        }

        public Action<HttpContext> this[string path]
        {
            get
            {
                if (contents.ContainsKey(path))
                    return contents[path];
                else return null;
            }
        }

        public void Add<TContext>(string basePath)
        {
            Type type = typeof(TContext);
            foreach (var method in type.GetMethods()
                    .Where(m => m.IsPublic && m.DeclaringType == type))
            {
                string path = $"{basePath}/{type.Name}/{method.Name}";
                var parameters = method.GetParameters();
                contents[path] = async httpContext => {

                    var context = httpContext.RequestServices.GetService<TContext>();
                    var handler = httpContext.RequestServices.GetService<JavaScriptHandler>();
                    if (context == null) return;
                    using (var reader = new StreamReader(httpContext.Request.Body))
                    {
                        string contents = reader.ReadToEnd();
                        handler.Compute(JavaScriptHandler.Create(contents));
                    }
                    var result = method.Invoke(context, new object[] { });
                    
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
                };

            }
        }

    }
}
