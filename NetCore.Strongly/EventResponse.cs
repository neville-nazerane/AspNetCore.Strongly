using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCore.Strongly
{

    public class AutoEventResponse<TContext>
        where TContext : class
    {

        public EventResponse EventResponse { get; set; }

        public static implicit operator AutoEventResponse<TContext>(Expression<Action<TContext>> toRun)
        {
            
            return null;
        }

    }

    public class EventResponse
    {


        internal string path;

        internal EventResponse()
        {

        }

    }

    public class EventResponse<TData> : EventResponse
    {

        public TData Data { get; internal set; }

        internal EventResponse()
        {
        }

    }

}
