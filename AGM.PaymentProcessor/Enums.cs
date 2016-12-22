using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.PaymentProcessor
{
    public enum TransactionType
    {
        Charge,
        Void,
        Refund,
        Auth,
        CaptureOnly
    }
}
