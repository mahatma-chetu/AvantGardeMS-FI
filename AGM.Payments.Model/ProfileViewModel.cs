using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Model
{
    public class ProfileViewModel
    {
        public int ProfileID { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        public string Address { get; set; }
        public string ProfileType { get; set; }
    }
}
