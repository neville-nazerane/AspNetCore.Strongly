using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace NetCore.Strongly.Services
{

    class JavaScriptHandler
    {
        private readonly IHtmlHelper html;
        private readonly IServiceProvider provider;

        public JavaScriptHandler(IHtmlHelper html, IServiceProvider provider)
        {
            this.html = html;
            this.provider = provider;
        }

        public void Compute(JavaScriptData data)
        {
            foreach (var bind in data.Bindings.Values)
                bind.Compute(provider);
        }

        public string Generate(JavaScriptData data)
        {
            string js = JsContent.content;
            if (data.Bindings.Count > 0)
            {
                js += $@"var stronglyData = {html.Raw(FormatObject(data))};";
            }
            return js;
        }

        public static JavaScriptData Create(string serializedData)
            => JsonConvert.DeserializeObject<JavaScriptData>(
                      WebUtility.HtmlDecode(serializedData), jsonSerializerSettings);

        const Formatting formatting = Formatting.None;
        static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        static string FormatObject(object obj) => JsonConvert.SerializeObject(obj, formatting, jsonSerializerSettings);

    }

    class JavaScriptData
    {

        public Dictionary<string, RecievedContext> Bindings { get; set; }

        public JavaScriptData()
        {
            Bindings = new Dictionary<string, RecievedContext>();
        }


    }
}
