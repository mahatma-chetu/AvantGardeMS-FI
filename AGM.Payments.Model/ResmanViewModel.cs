using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class ResmanViewModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage = "Integration Partner ID Required")]
        [Display(Name = "Integration Partner ID")]
        public string IntegrationPartnerID { get; set; }

        [Display(Name = "Api Key")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Api Key Required")]
        public string ApiKey { get; set; }

        [Display(Name = "Account ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account ID Required")]
        public string AccountID { get; set; }
    }
}