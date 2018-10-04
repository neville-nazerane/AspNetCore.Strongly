using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Strongly
{
    public class PropertyBindingResponse
    {

        internal PropertyContext Context { get; set; }

        internal PropertyBindingResponse(PropertyContext context)
        {
            Context = context;
        }

    }
}
