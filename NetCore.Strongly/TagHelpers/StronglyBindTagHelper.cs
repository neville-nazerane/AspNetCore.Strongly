using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Strongly.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetCore.Strongly.TagHelpers
{

    [HtmlTargetElement("form")]
    public class StronglyBindTagHelper : TagHelper
    {
        private readonly JavaScriptData jsData;

        public PropertyBindingResponse StBind { get; set; }

        public StronglyBindTagHelper(IServiceProvider provider)
        {
            jsData = provider.GetService<JavaScriptData>();
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (StBind != null)
            {
                string key = Guid.NewGuid().ToString();
                output.Attributes.SetAttribute("binding-key", key);
                jsData.Bindings.Add(key, StBind.Context);
            }

        }

    }
}
