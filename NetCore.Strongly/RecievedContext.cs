using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace NetCore.Strongly
{
    class RecievedContext
    {

        public string ClassName { get; set; }

        public string PropertyName { get; set; }

        public string TypeName { get; set; }

        public string Data { get; set; }

        public void Compute(IServiceProvider provider)
        {
            var value = JsonConvert.DeserializeObject(Data, Type.GetType(TypeName));
            Type targetType = Type.GetType(ClassName);
            var target = provider.GetService(targetType);
            targetType.GetProperty(PropertyName).SetValue(target, value);
        }

    }
}
