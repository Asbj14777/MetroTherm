using MetroTherm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private ObservableCollection<EquipmentViewModel> _customerEquipments = new();
        public ObservableCollection<EquipmentViewModel> CustomerEquipments
        {
            get => _customerEquipments;
            set { _customerEquipments = value; OnPropertyChanged(); }
        }

        private double _customerUsage;
        public double CustomerUsage
        {
            get
            {
                double usage = 0;
                foreach (EquipmentViewModel eq in CustomerEquipments)
                {
                    if (double.TryParse(eq.Value, out double v))
                        usage += v;
                }
                return usage;
            }
            set { _customerUsage = value; OnPropertyChanged(); }

        }

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }

    }
}
