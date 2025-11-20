using System;

namespace MetroTherm.Models
{
    public class Invoice
    {
        public double Price { get; set; }
        public DateTime Period { get; set; }
        public double Usage { get; set; }
        public string ProductName { get; set; }
        public int InvoiceNumber { get; set; }

        public Invoice(double price, DateTime period, double usage, string product, int invoicenumber)
        {
            Price = price;
            Period = period;
            Usage = usage;
            ProductName = product;
            InvoiceNumber = invoicenumber;
        }
        public override string ToString() => $"Price: {Price}, Period: {Period}, Usage: {Usage}, ProductName: {ProductName}, InvoiceNumber: {InvoiceNumber}"; 
        
        
    }
}
