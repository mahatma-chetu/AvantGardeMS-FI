using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class AuthAccount
    {
        [Required(ErrorMessage ="Api Id is required!")]
        public string ApiID { get; set; }
        [Required(ErrorMessage = "Api Key is required!")]
        public string ApiKey { get; set; }       
        public int AuthAccountID { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string AuthAccountName { get; set; }
        public string PropertyID { get;  set; }
        public string PropertyName { get; set; }
    }
}