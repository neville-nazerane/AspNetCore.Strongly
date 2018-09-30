using System;
using System.IO;
using System.Reflection;

namespace ClientConfig
{
    class Program
    {
        static void Main(string[] args)
        {


            // string root = "../../..";
            string root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../../..";
            
            string contents = File.ReadAllText($@"{root}/js/strongly.min.js");
            contents = $"\"{contents.Replace("\"", "\"\"")}\"";
            string filePath = $@"{root}/../NetCore.Strongly/Services/JsContent.g.cs";

            File.WriteAllText(filePath,
                                $@"
namespace NetCore.Strongly.Services
{{
    partial class JsContent
    {{
        public const string content = @{contents};
    }}
}}
                         ");

        }
    }
}
