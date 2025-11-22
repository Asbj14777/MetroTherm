using MetroTherm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MetroTherm.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly CustomerRepository customerRepo;
        private readonly EquipmentRepository equipmentRepo;
        private readonly InvoiceRepository invoiceRepository;
        private readonly IDataHandler _handler; 


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

        private EquipmentViewModel selectedEquipment;
        public EquipmentViewModel SelectedEquipment
        {
            get => selectedEquipment;
            set { selectedEquipment = value; OnPropertyChanged(); }
        }
     
        public ICommand GenerateInvoice {  get; }   
        public ICommand GetCalculations {  get; }
        public MainViewModel()
        {
            equipmentRepo = new EquipmentRepository();
            
            customerRepo = new CustomerRepository();
            
            invoiceRepository = new InvoiceRepository();
            
            _handler = new DataHandler(null); 
            
            foreach (Equipment equipment in equipmentRepo.GetAll())
                Equipments.Add(new EquipmentViewModel(equipment));

            foreach (Customer customer in customerRepo.GetAll())
                Customers.Add(new CustomerViewModel(customer));

            GetCalculations = new RelayCommand(
                execute: _ => getCalculations(),
                canExecute: _ => BillingCustomer != null
            );
            GenerateInvoice = new RelayCommand(
                execute: _ => SaveInvoice(),
                canExecute: _ => BillingCustomer != null && Subtotal > 1
            );
      
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
            set {
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
               if(_vat == value) 
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
                if(_total == value) 
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
                ("Heat Energy E1",     PricePerKwh1),
                ("Cooling energy E3",  PricePerKwh2),
                ("MC2 Heat energy E1", PricePerKwh3)
            };

            foreach (var item in energyItems)
            {
                double kwh = equipmentRepo.GetTotalKwh(BillingCustomer, item.Type, FromDate, ToDate);
                subtotal += kwh * item.PricePerKwh;
            }

            Subtotal =      subtotal;
            Vat =           (Subtotal * vat);
            Total =         (Subtotal + Vat);
        }
        private void SaveInvoice()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=================== BØRGE'S VARME ================");
            sb.AppendLine("                       FAKTURA");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Kunde      : {BillingCustomer.Name}");
            sb.AppendLine($"Adresse    :  {BillingCustomer.Address}");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Tids Periode : {FromDate:dd-MM-yyyy} til {ToDate:dd-MM-yyyy}");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Subtotal   : {Subtotal:F2} kr");
            sb.AppendLine($"Moms (25%) : {Vat:F2} kr");
            sb.AppendLine(new string('-', 50));
            sb.AppendLine($"Total inkl. moms: {Total:F2} kr");
            sb.AppendLine(new string('=', 50));

            if(_handler.SaveData(sb.ToString()))
                MessageBox.Show("Faktura Gemt");
        }
    }
}
