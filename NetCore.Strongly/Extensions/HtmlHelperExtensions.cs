using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.Strongly.Extensions
{
    public static class HtmlHelperExtensions
    {

        public static EventResponse<T> Strongly<TContext, T>(this IHtmlHelper<TContext> htmlHelper, Expression<Func<TContext, T>> toRun)
            where TContext : class
            => htmlHelper.ViewContext.HttpContext.RequestServices
                    .GetService<StronglyControl<TContext>>().Run(toRun);
    }
}
