using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Strongly.Extensions
{
    public class StronglyServiceOptions
    {
        private readonly IServiceCollection services;

        internal StronglyServiceOptions(IServiceCollection services)
        {
            this.services = services;
            //mappingActions = new List<Action<PathMappings>>();
        }

        //internal List<Action<PathMappings>> mappingActions;

        //public StronglyServiceOptions Add<TContext>()
        //{
        //    mappingActions.Add(map => map.Add<TContext>("/strongly"));
        //    return this;
        //}

    }
}
