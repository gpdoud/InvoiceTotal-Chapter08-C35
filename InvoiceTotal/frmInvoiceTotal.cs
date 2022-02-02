using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceTotal {

    public partial class frmInvoiceTotal : Form {
        public frmInvoiceTotal() {
            InitializeComponent();
        }

        //8.1.2 declare class variables for array and list here
        List<decimal> invoiceTotals = new List<decimal>();

        private void btnCalculate_Click(object sender, EventArgs e) {
            try {

                if (txtSubtotal.Text == "") {
                    MessageBox.Show(
                        "Subtotal is a required field.", "Entry Error");
                } else {
                    decimal subtotal = Decimal.Parse(txtSubtotal.Text);
                    if (subtotal > 0 && subtotal < 10000) {
                        decimal discountPercent = 0m;
                        if (subtotal >= 500)
                            discountPercent = .2m;
                        else if (subtotal >= 250 & subtotal < 500)
                            discountPercent = .15m;
                        else if (subtotal >= 100 & subtotal < 250)
                            discountPercent = .1m;
                        decimal discountAmount = subtotal * discountPercent;
                        decimal invoiceTotal = subtotal - discountAmount;

                        discountAmount = Math.Round(discountAmount, 2);
                        invoiceTotal = Math.Round(invoiceTotal, 2);

                        //8.1.6 try..catch if array is overrun
                        try {
                            //8.1.2 add invoice total to array
                            invoiceTotals.Add(invoiceTotal);
                        }catch(Exception) {
                            MessageBox.Show("Index is full. Invoice totals ignored", "Invoice Totals");
                        }

                        txtDiscountPercent.Text = discountPercent.ToString("p1");
                        txtDiscountAmount.Text = discountAmount.ToString();
                        txtTotal.Text = invoiceTotal.ToString();

                    } else {
                        MessageBox.Show(
                            "Subtotal must be greater than 0 and less than 10,000.",
                            "Entry Error");
                    }
                }
            } catch (FormatException) {
                MessageBox.Show(
                    "Please enter a valid number for the Subtotal field.",
                    "Entry Error");
            }
            txtSubtotal.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            // TODO: add code that displays dialog boxes here
            string message = string.Empty;
            //8.1.7 Sort the array
            invoiceTotals.Sort();
            foreach (decimal invoiceTotal in invoiceTotals) {
                if (invoiceTotal > 0)
                    message += invoiceTotal.ToString("c") + "\n";
            }
            MessageBox.Show(message, "Invoice Totals");

            this.Close();
        }

    }
}
