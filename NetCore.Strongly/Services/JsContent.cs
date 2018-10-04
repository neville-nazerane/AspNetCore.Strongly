using NetCore.Strongly.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Strongly.Services
{
    partial class JsContent
    {

        internal const string allContent = constants + content;

        internal const string constants = @"const 
                                stronglyStartPath='" + StronglyMiddleware.startPath + @"'
                            ;";

    }
}
