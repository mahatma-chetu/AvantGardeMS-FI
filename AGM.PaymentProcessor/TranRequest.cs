using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.PaymentProcessor
{
    public class TranRequest
    {
        public string CardNumber { get; set; }
        public string CardExpiry{ get; set; }
        public string CardVerificationValue { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TransactionID { get; set; }
    }    
}
