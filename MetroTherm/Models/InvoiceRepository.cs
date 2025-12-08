using MetroTherm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MetroTherm.Models
{
    public class InvoiceRepository
    { 
        private readonly List<Invoice> invoices = new();

        public InvoiceRepository() 
        {

            
        }

        // add invoice
        public void AddInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice)); 
            invoices.Add(invoice);
        }

        // delete invoice
        public void DeleteInvoice(int invoiceNumber) 
        {
          
            var invoice = invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
            {
                invoices.Remove(invoice);
            }

        }

        // choose invoice
        public Invoice? SelectInvoice(int invoiceNumber)
        {
            if (invoiceNumber <= 0) return null;
            return invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
        }

        // update invoice
        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));
            var index = invoices.FindIndex(i => i.InvoiceNumber == invoice.InvoiceNumber); 
            if (index >= 0) 
            {
                invoices[index] = invoice;
            }
        }

        // get invoice
        public List<Invoice> GetAll()
        {
            return invoices;
        }

        public bool GenerateInvoice(
            string name,
            string address,
            DateTime fromDate,
            DateTime toDate,
            double subtotal,
            double vat,
            double total)
        {
            int invoiceNumber;

            if (invoices.Count > 0)
            {
                // find highest invoice number if we already made previous ones
                int max = invoices.Max(i => i.InvoiceNumber);
                invoiceNumber = max + 1;
            }
            else
            {
                invoiceNumber = 1;
            }
            string path = $"Faktura{invoiceNumber}_{DateTime.Now:yyyMMdd_HHmmss}.txt";

            // creates invoice and adds it to repository
            Invoice invoice = new Invoice(invoiceNumber, name, address, fromDate, toDate, subtotal, vat, total);
            AddInvoice(invoice);

            // saves invoice
            IDataHandler dataHandler = new DataHandler(path);
            bool saved = dataHandler.SaveData(invoice.ToString());

            return saved; // return true if saved, false otherwise.
        }
    
    }
}