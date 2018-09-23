using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Strongly
{
    public class PropertyBindingResponse
    {

        internal RecievedContext Context { get; set; }

        internal PropertyBindingResponse(RecievedContext context)
        {
            Context = context;
        }

    }
}
