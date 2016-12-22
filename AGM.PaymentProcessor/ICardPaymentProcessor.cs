using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.PaymentProcessor
{
    public interface ICardPaymentProcessor
    {
        TranResponse MakePayment(TranRequest transactionRequest, string apiID, string apiKey);
    }
}
