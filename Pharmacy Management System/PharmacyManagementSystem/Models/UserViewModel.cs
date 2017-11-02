using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage="*Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Email is required")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage="*Please Select Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage="*Date of Birth is required")]
        public System.DateTime Date_of_Birth { get; set; }

        [Required(ErrorMessage="*Age is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "*Invalid Input(Only Number)")]
        public int Age { get; set; }

        [Required(ErrorMessage = "*Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*Contact is required")]
        [RegularExpression(@"^(?:\+88|01)?\d{11}$", ErrorMessage = "Please enter a valid Phone Number")]
        public string Contact { get; set; }

        [Required(ErrorMessage="*Please Select Blood Group")]
        public string Blood_Group { get; set; }

        [Required(ErrorMessage = "*Please Select Marital Status")]
        public string Marital_Status { get; set; }

        [Required(ErrorMessage = "*Join Date is Required")]
        public System.DateTime Join_Date { get; set; }

        [Required(ErrorMessage = "*Salary is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "*Invalid Input(Only Number)")]
        public int Salary { get; set; }

        [Required(ErrorMessage="*Username is required")]
        public string Username { get; set; }
        
        //Custom Attribute
        [Required(ErrorMessage = "*Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*Please Select Usertype")]
        public string Usertype { get; set; }
    }
}