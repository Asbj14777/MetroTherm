using MetroTherm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Globalization;

namespace MetroTherm.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly CustomerRepository customerRepo;
        private readonly EquipmentRepository equipmentRepo;

        private ObservableCollection<EquipmentViewModel> _equipments = new();
        public ObservableCollection<EquipmentViewModel> Equipments
        { 
            get { return _equipments; } 
            set { _equipments = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CustomerViewModel> _customers = new();
        public ObservableCollection<CustomerViewModel> Customers 
        { 
            get { return _customers;  }
            set { _customers = value; OnPropertyChanged(); } 
        }

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set 
            { 
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value; 
                    OnPropertyChanged();
                }
            }
        }

        private EquipmentViewModel selectedEquipment;
        public EquipmentViewModel SelectedEquipment
        {
            get => selectedEquipment;
            set { selectedEquipment = value; OnPropertyChanged(); }
        }
        public ICommand ShowMessageCommand { get; }

        private void ShowMessage()
        {
            MessageBox.Show(SelectedEquipment.ParameterName);
        }

        public MainViewModel()
        {
            equipmentRepo = new EquipmentRepository();
            customerRepo = new CustomerRepository();

            foreach (Equipment equipment in equipmentRepo.GetAll())
                Equipments.Add(new EquipmentViewModel(equipment));

            foreach (Customer customer in customerRepo.GetAll())
                Customers.Add(new CustomerViewModel(customer));

            AssignCustomerEquipment(); // assigns equipments to each customer

            ShowMessageCommand = new RelayCommand(
                execute: _ => ShowMessage(),
                canExecute: _ => true
            );

        }

        public void showEquipment(Equipment equipment)
        {

        }

        public EquipmentViewModel chooseEquipment(EquipmentViewModel equipment)
        {
            return equipment;
        }

        private CustomerViewModel _billingCustomer;
        public CustomerViewModel BillingCustomer
        {
            get => _billingCustomer;
            set { _billingCustomer = value; OnPropertyChanged(); }
        }

        private EquipmentViewModel _billingEquipment;
        public EquipmentViewModel BillingEquipment
        {
            get => _billingEquipment;
            set { _billingEquipment = value; OnPropertyChanged(); }
        }
        private double _pricePerKwh1;
        public double PricePerKwh1
        {
            get => _pricePerKwh1;
            set { _pricePerKwh1 = value; OnPropertyChanged(); }
        }

        private double _pricePerKwh2;
        public double PricePerKwh2
        {
            get => _pricePerKwh2;
            set { _pricePerKwh2 = value; OnPropertyChanged(); }
        }

        private double _pricePerKwh3;
        public double PricePerKwh3
        {
            get => _pricePerKwh3;
            set { _pricePerKwh3 = value; OnPropertyChanged(); }
        }

        private void AssignCustomerEquipment()
        {
            foreach (CustomerViewModel customer in Customers)
            {
                foreach (EquipmentViewModel eq in Equipments)
                {
                    // checks if device id is same as customer id
                    if (eq.DeviceId == customer.ID)
                    {
                        customer.CustomerEquipments.Add(eq);
                    }
                }
            }
        }        
    }
}
