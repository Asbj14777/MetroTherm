using System;
using System.Data;
using System.Text;

namespace MetroTherm.Models
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        
        public double Subtotal { get; set; }
        public double Vat {  get; set; }
        public double Total { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Invoice(int invoiceNumber, string customerName, string customerAddress, DateTime from, DateTime to, double subtotal, double vat, double total)
        {
            InvoiceNumber = invoiceNumber;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            FromDate = from;
            ToDate = to;
            Subtotal = subtotal;
            Vat = vat;
            Total = total;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=================== BØRGE'S VARME ================");
            sb.AppendLine("                       FAKTURA");
            sb.AppendLine($"Oprettet   : {CreatedOn:dd-MM-yyy HH:mm}");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Kunde      : {CustomerName}");
            sb.AppendLine($"Adresse    :  {CustomerAddress}");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Tids Periode : {FromDate:dd-MM-yyyy} til {ToDate:dd-MM-yyyy}");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Subtotal   : {Subtotal:F2} kr");
            sb.AppendLine($"Moms (25%) : {Vat:F2} kr");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Total inkl. moms: {Total:F2} kr");
            sb.AppendLine(new string('=', 50));

            return sb.ToString();
        }
    }
}
