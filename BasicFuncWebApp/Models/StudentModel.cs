using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicFuncWebApp.Models
{
    public class StudentModel
    {
        [Display(AutoGenerateField = true, Name = "ID")]
        //[HiddenInput(DisplayValue = false)]
        public int sid { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }

    public class Student
    {
        public int sid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
    }
}
