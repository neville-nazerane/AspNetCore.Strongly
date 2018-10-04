using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Strongly.Extensions;
using NetCore.Strongly.Services;

namespace NetCore.Strongly
{

    public class StronglyControl<TContext>
        where TContext : class
    {
        private readonly TypeHandler typeHandler;

        public StronglyControl(IServiceProvider provider)
        {
            typeHandler = provider.GetService<TypeHandler>();
        }

        public PropertyBindingResponse Bind<T>(Expression<Func<TContext, T>> expression)
        {
            if (expression.Body is MemberExpression member)
                return new PropertyBindingResponse(new PropertyContext {
                    Key = typeHandler.GetPropertyKey(expression)
                });
            else throw new InvalidOperationException("Invalid lamda provided. only properties or fields allowed");
        }

        public EventResponse<T> Run<T>(Expression<Func<TContext, T>> toRun)
            => new EventResponse<T>
            {
                path = $"{StronglyMiddleware.startPath}/{typeHandler.GetMethodKey(toRun)}"
            };

        public EventResponse Run(Expression<Action<TContext>> toRun)
            => new EventResponse {
                path = $"{StronglyMiddleware.startPath}/{typeHandler.GetMethodKey(toRun)}"
            };

    }
}
