using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace MetroTherm.Models
{
    public class Invoice : INotifyPropertyChanged
    {
        private double _price;
        private DateTime _period;
        private double _usage;
        private string _productName; 
        private int invoiceNumber;

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime Period
        {
            get { return _period; }
            set
            {
                if (_period != value)
                {
                    _period = value;
                    NotifyPropertyChanged();
                }
            }
        }   

        public double Usage
        {
            get { return _usage; }
            set
            {
                if (_usage != value)
                {
                    _usage = value;
                    NotifyPropertyChanged();
                }
            }
        }   

        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                if (invoiceNumber != value)
                {
                    invoiceNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Invoice(double price, DateTime period, double usage, string product, int invoicenumber)
        {
            _price = price;
            _period = period;
            _usage = usage;
            _productName = product;
            invoiceNumber = invoicenumber;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
