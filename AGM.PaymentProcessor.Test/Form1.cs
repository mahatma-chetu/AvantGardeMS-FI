using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGM.PaymentProcessor.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void MakePayment(string cardNumber,string cardExpiry, decimal amount , string transactionType)
        {
            //Console.WriteLine("Authorize Credit Card Sample");
            TransactionType typeOfTransaction = (TransactionType)Enum.Parse(typeof(TransactionType), transactionType);
            ICardPaymentProcessor payment = Processor.CreatePaymentProcessor();
            string transactionID = string.Empty;
            if (typeOfTransaction== TransactionType.Void 
                || typeOfTransaction== TransactionType.Refund
                || typeOfTransaction == TransactionType.CaptureOnly)
            {
                transactionID = txtTransactionID.Text;
            }
            TranRequest tr = new TranRequest { CardNumber = cardNumber, CardExpiry = cardExpiry, Amount = amount, TransactionType = typeOfTransaction ,TransactionID =transactionID};
            var response = payment.MakePayment(tr, "8k4gSGmh57L4", "5Pwvrq95j3Jp2Q64");
            string message = GetMessage(response);
            txtResponse.Text = message;
        }
        public static string GetMessage(TranResponse response)
        {
            Func<string> temp = () =>
            {
                StringBuilder sb = new StringBuilder();
                foreach (var property in response.GetType().GetProperties())
                {
                    var propValue = property.GetValue(response);
                    string value = propValue == null ? string.Empty : propValue.ToString();
                    sb.AppendLine(property.Name + " : " + propValue);

                }
                return sb.ToString();
            };
            return temp();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCardNumber.Text) 
                && !string.IsNullOrEmpty(txtExpiry.Text) 
                && !string.IsNullOrEmpty(txtAmount.Text)
                && !string.IsNullOrEmpty(cmbTransactionType.SelectedItem.ToString()))
            {
                string cardNumber = txtCardNumber.Text;
                string cardExpiry = txtExpiry.Text;
                decimal amount = Convert.ToDecimal(txtAmount.Text);
                MakePayment(cardNumber, cardExpiry, amount, cmbTransactionType.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Error");
            }
           

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
