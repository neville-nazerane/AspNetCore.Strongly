using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public class SampleService
    {

        public Employee Employee { get; set; }

        public void Edit()
        {
            
        }

        public string NewStuff() => $"Hello {Employee.FirstName} {Employee.LastName}";

        public bool Edited() => true;

    }
}
