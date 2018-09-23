using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCore.Strongly.TagHelpers
{

    [HtmlTargetElement("div")]
    [HtmlTargetElement("span")]
    [HtmlTargetElement("button")]
    [HtmlTargetElement("a")]
    public class StronglyClickTagHelper : TagHelper
    {

        public EventResponse StClick { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            
            if (StClick != null)
                output.Attributes.SetAttribute("onClick",
                            $"stronglySent('{StClick.path}')");
        }

    }
}
