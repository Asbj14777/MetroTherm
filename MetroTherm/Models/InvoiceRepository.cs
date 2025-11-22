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
        private readonly IDataHandler _handler;        
        private readonly List<Invoice> _invoices = new(); 
        private readonly MainViewModel _mainViewModel;     
        private readonly EquipmentRepository _equipmentRepository;      

        public InvoiceRepository(MainViewModel mvm) 
        {
            _mainViewModel = mvm;
            
        }

        // Tilføj en faktura
        public void AddInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice)); 
            _invoices.Add(invoice);
        }

        // Slet en faktura 
        public void DeleteInvoice(int invoiceNumber) 
        {
          
            var invoice = _invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
            {
                _invoices.Remove(invoice);
            }

        }

        // Vælg en faktura
        public Invoice? SelectInvoice(int invoiceNumber)
        {
            if (invoiceNumber <= 0) return null;
            return _invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
        }

        // Opdater en faktura
        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));
            var index = _invoices.FindIndex(i => i.InvoiceNumber == invoice.InvoiceNumber); 
            if (index >= 0) 
            {
                _invoices[index] = invoice;
            }
        }

        // Hent alle fakturaer
        public List<Invoice> GetAll()
        {
            return new List<Invoice>(_invoices);
        }


    
    }
}