using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestCommon.Data;
using TestCommon.Models;
using NetCore.Strongly;
using TestCommon.Services;

namespace TestWebApp.Pages
{
    public class StronglyModel : PageModel
    {
        private readonly StronglyControl<ICommentRepository> control;

        public StronglyModel(StronglyControl<ICommentRepository> control)
        {
            this.control = control;
        }

        public PropertyBindingResponse CommentBind => control.Bind(c => c.ToAdd);

        public EventResponse<Comment> AddEvent => control.Run(c => c.Add());

        [BindProperty]
        public Comment Comment { get; set; }
        
    }
}