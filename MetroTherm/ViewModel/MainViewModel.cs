using MetroTherm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
        private readonly InvoiceRepository invoiceRepository;


        private ObservableCollection<EquipmentViewModel> _equipments = new();
        public ObservableCollection<EquipmentViewModel> Equipments
        {
            get { return _equipments; }
            set { _equipments = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CustomerViewModel> _customers = new();
        public ObservableCollection<CustomerViewModel> Customers
        {
            get { return _customers; }
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
                    BillingCustomer = _selectedCustomer;
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

        public ICommand GenerateInvoice { get; }
        public ICommand GetCalculations { get; }
        public ICommand ShowEquipment { get; }

        public MainViewModel()
        {
            equipmentRepo = new EquipmentRepository();
            customerRepo = new CustomerRepository();
            invoiceRepository = new InvoiceRepository();

            foreach (Equipment equipment in equipmentRepo.GetAll())
                Equipments.Add(new EquipmentViewModel(equipment));

            foreach (Customer customer in customerRepo.GetAll())
                Customers.Add(new CustomerViewModel(customer));

            AssignCustomerEquipment(); // assigns equipments to each customer

            GetCalculations = new RelayCommand(
                execute: _ => getCalculations(),
                canExecute: _ => BillingCustomer != null
            );
            GenerateInvoice = new RelayCommand(
                execute: _ => SaveInvoice(),
                canExecute: _ => BillingCustomer != null && Subtotal > 1
            );

            ShowEquipment = new RelayCommand(execute: _ => showEquipment(), canExecute: _ => SelectedEquipment != null); 


        }


        private void showEquipment()
        {
            MessageBox.Show($"{SelectedEquipment.Value}, {SelectedEquipment.ParameterName}");
        }

        private CustomerViewModel _billingCustomer;
        public CustomerViewModel BillingCustomer
        {
            get => _billingCustomer;
            set
            {
                if (_billingCustomer != value)
                {
                    _billingCustomer = value;
                    UpdateBillingEquipments();
                    OnPropertyChanged();
                }
            }
        }

        private EquipmentViewModel _billingEquipment;
        public EquipmentViewModel BillingEquipment
        {
            get => _billingEquipment;
            set { _billingEquipment = value; OnPropertyChanged(); }
        }
        private double _pricePerKwh1 = 0.1;
        public double PricePerKwh1
        {
            get => _pricePerKwh1;
            set { _pricePerKwh1 = value; OnPropertyChanged(); }
        }

        private double _pricePerKwh2 = 0.1;
        public double PricePerKwh2
        {
            get => _pricePerKwh2;
            set { _pricePerKwh2 = value; OnPropertyChanged(); }
        }

        private double _pricePerKwh3 = 0.1;
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
        private DateTime _fromDate = DateTime.Today;
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                if (_fromDate == value)
                    return;

                _fromDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _toDate = DateTime.Today.AddDays(1);
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                if (_toDate == value)
                    return;

                _toDate = value;
                OnPropertyChanged();
            }
        }

        private double _subtotal;
        public double Subtotal
        {
            get => _subtotal;
            set
            {
                if (_subtotal == value)
                    return;
                _subtotal = value;
                OnPropertyChanged();
            }
        }

        private double _vat;
        public double Vat
        {
            get => _vat;
            set
            {
                if (_vat == value)
                    return;
                _vat = value;
                OnPropertyChanged();
            }
        }

        private double _total;
        public double Total
        {
            get => _total;
            set
            {
                if (_total == value)
                    return;
                _total = value;
                OnPropertyChanged();
            }
        }

        private void getCalculations()
        {
            double vat = 0.25;
            double subtotal = 0.0;

            var energyItems = new List<(string Type, double PricePerKwh)>
            {
                ("Heat energy E1",     PricePerKwh1),
                ("Cooling energy E3",  PricePerKwh2),
                ("MC2 Heat energy E1", PricePerKwh3),
                ("Heat energy E2",     PricePerKwh1),
                ("Cooling energy E4",  PricePerKwh2),
                ("MC2 Heat energy E2", PricePerKwh3)
            };

            foreach (var item in energyItems)
            {
                double totalKwh = 0.0;
                // calculates totalKwh per item
                foreach (EquipmentViewModel eq in BillingCustomer.CustomerEquipments)
                {
                    // checks if parameter type matches item type
                    if (eq.ParameterName == item.Type)
                    {
                        // checks date range
                        DateTime timestamp;
                        if (DateTime.TryParse(eq.Timestamp, out timestamp))
                        {
                            if (timestamp >= FromDate && timestamp <= ToDate)
                            {
                                // parses kwh value
                                double value;
                                if (double.TryParse(eq.Value, out value))
                                {
                                    totalKwh += value;
                                }
                            }
                        }
                    }
                }
                // add to subtotal
                subtotal += totalKwh * item.PricePerKwh;
            }
            Subtotal = subtotal;
            Vat = (Subtotal * vat);
            Total = (Subtotal + Vat);
        }

        private void SaveInvoice()
        {
            bool saved = invoiceRepository.GenerateInvoice(
                BillingCustomer.Name,
                BillingCustomer.Address,
                FromDate,
                ToDate,
                Subtotal, 
                Vat,
                Total
                );

            if (saved)
                MessageBox.Show("faktura sendt");
            else
                MessageBox.Show("kunne ikke sendes. prøv igen");
        }

        private string _modelName1;
        public string ModelName1
        {
            get => _modelName1;
            set { _modelName1 = value; OnPropertyChanged(); }
        }

        private string _modelName2;
        public string ModelName2
        {
            get => _modelName2;
            set { _modelName2 = value; OnPropertyChanged(); }
        }

        private string _modelName3;
        public string ModelName3
        {
            get => _modelName3;
            set { _modelName3 = value; OnPropertyChanged(); }
        }

        private ObservableCollection<EquipmentViewModel> _billingEquipments = new();
        public ObservableCollection<EquipmentViewModel> BillingEquipments
        {
            get => _billingEquipments;
            set { _billingEquipments = value; OnPropertyChanged(); }
        }

        public void UpdateBillingEquipments()
        {
            if (BillingCustomer == null)
            {
                BillingEquipments = new ObservableCollection<EquipmentViewModel>();
                ModelName1 = ModelName2 = ModelName3 = "";
                return;
            }

            // only finds equipment unique by name and no duplicate
            List<EquipmentViewModel> models = new List<EquipmentViewModel>();
            foreach (EquipmentViewModel eq in BillingCustomer.CustomerEquipments)
            {
                bool exist = false;
                foreach(EquipmentViewModel m in models)
                {
                    if (m.ParameterName == eq.ParameterName)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    models.Add(eq);
            }

            BillingEquipments = new ObservableCollection<EquipmentViewModel>(models);

            ModelName1 = models.ElementAtOrDefault(0)?.ParameterName ?? "";
            ModelName2 = models.ElementAtOrDefault(1)?.ParameterName ?? "";
            ModelName3 = models.ElementAtOrDefault(2)?.ParameterName ?? "";
        }
    }
}
