using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.PaymentProcessor
{
    public class TranResponse
    {
        public string TransactionMessage { get; set; }
        public string ResponseCode { get; set; }
        public string AuthCode { get; set; }
        public string TransactionID { get; internal set; }
        public string MessageCode { get; internal set; }
        public string Description { get; internal set; }
    }
}
