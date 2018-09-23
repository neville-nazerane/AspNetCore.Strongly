using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.Strongly
{

    public class StronglyControl<TContext>
        where TContext : class
    {
        readonly TContext _context;

        public StronglyControl(IServiceProvider provider)
        {
            _context = provider.GetService<TContext>();
        }

        public PropertyBindingResponse Bind<T>(Expression<Func<TContext, T>> expression)
        {
            if (expression.Body is MemberExpression member)
                return new PropertyBindingResponse(new RecievedContext {
                    ClassName = typeof(TContext).AssemblyQualifiedName,
                    PropertyName = member.Member.Name,
                    TypeName = typeof(T).AssemblyQualifiedName
                });
            else throw new InvalidOperationException("Invalid lamda provided. only properties or fields allowed");
        }

        public EventResponse<T> Run<T>(Expression<Func<TContext, T>> toRun)
        {
            if (toRun.Body is MethodCallExpression method)
                return new EventResponse<T> {
                    path = $"/strongly/{typeof(TContext).Name}/{method.Method.Name}"
                };   
            throw new InvalidOperationException("Invalid lamda provided. function is expected.");
        }

        public EventResponse Run(Expression<Action<TContext>> toRun)
        {
            var result = new EventResponse();
            toRun.Compile()(_context);
            return result;
        }

    }
}
