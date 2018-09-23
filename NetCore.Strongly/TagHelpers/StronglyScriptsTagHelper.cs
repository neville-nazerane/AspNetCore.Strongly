using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Strongly.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Strongly.TagHelpers
{
    public class StronglyScriptsTagHelper : TagHelper
    {
        private readonly JavaScriptData data;
        private readonly JavaScriptHandler handler;

        public StronglyScriptsTagHelper(IServiceProvider service)
        {
            data = service.GetService<JavaScriptData>();
            handler = service.GetService<JavaScriptHandler>();
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "script";
            output.Content.SetHtmlContent(handler.Generate(data));
        }

    }
}
