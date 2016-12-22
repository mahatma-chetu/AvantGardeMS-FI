using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.PaymentProcessor
{
    public class Processor
    {
        public static ICardPaymentProcessor CreatePaymentProcessor()
        {
            return new CardPaymentProcessor();
        }
    }
}
