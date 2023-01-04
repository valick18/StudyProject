using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models
{
    public class RegistrationModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}