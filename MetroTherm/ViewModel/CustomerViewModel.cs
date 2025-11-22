using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroTherm.Models;

namespace MetroTherm.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly Customer _customer;

        // customer properties exposed to view
        public string ID
        {
            get { return _customer.ID; }
            set { _customer.ID = value; OnPropertyChanged(); }

        }

        public string Name
        {
            get { return _customer.Name; }
            set { _customer.Name = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get { return _customer.Address; }
            set { _customer.Address = value; OnPropertyChanged(); }
        }

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }
    }
}
