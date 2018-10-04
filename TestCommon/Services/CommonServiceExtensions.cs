using NetCore.Strongly.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using TestCommon.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CommonServiceExtensions
    {

        public static IServiceCollection AddCommon(this IServiceCollection services)
            => services.AddScoped<ICommentRepository, CommentRepository>();

    }
}
