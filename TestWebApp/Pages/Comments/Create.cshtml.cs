using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestCommon.Data;
using TestCommon.Models;
using TestCommon.Services;

namespace TestWebApp.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly ICommentRepository repository;

        public CreateModel(ICommentRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Comment Comment { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            repository.Add(Comment);
            return RedirectToPage("./Index");
        }
    }
}