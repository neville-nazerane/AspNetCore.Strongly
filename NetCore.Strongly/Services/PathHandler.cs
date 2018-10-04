using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NetCore.Strongly.Services
{
    class PathHandler
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TypeHandler typeHandler;

        public PathHandler(IServiceProvider serviceProvider, TypeHandler typeHandler)
        {
            this.serviceProvider = serviceProvider;
            this.typeHandler = typeHandler;
        }

        internal PathResult Execute(string path, JavaScriptData javaScriptData)
        {
            if (path.StartsWith("/")) path = path.Substring(1);

            foreach (var binding in javaScriptData.Bindings.Values)
                if (!typeHandler.TrySet(binding, serviceProvider))
                    throw new InvalidOperationException($"Unable to assign value '{binding.RawData}' to property");

            if (typeHandler.TryExecute(path, serviceProvider, out var data))
                return new PathResult
                {
                    Data = data,
                    Successful = true
                };
            else return false;
        }

        internal class PathResult
        {
            public object Data { get; set; }

            public bool Successful { get; set; }

            public static implicit operator PathResult(bool successful)
                => new PathResult { Successful = successful };

            public static implicit operator bool(PathResult result)
                => result.Successful;

        }

    }
}
