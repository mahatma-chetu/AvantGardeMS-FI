using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Model
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Employees = new List<Employee>();
        }
        public List<Employee> Employees { get; set; }   
    }

    public class Employee
    {
        public Employee()
        {
            Properties = new List<Option>();
        }
        public List<Option> Properties { get; set; }

        [Required(ErrorMessage ="Employee id is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        public string Address { get; set; }
    }

    public class Option
    {
        public string DisplayText { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
    }
}
