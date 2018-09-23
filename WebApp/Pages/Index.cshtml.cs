using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCore.Strongly;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel(StronglyControl<SampleService> control)
        {
            NewStuff = control.Run(s => s.NewStuff());
            Edited = control.Run(s => s.Edited());
            EmployeeBind = control.Bind(s => s.Employee);
        }

        public EventResponse NewStuff { get; }

        public EventResponse Edited { get; }

        public PropertyBindingResponse EmployeeBind { get; }

    }
}
