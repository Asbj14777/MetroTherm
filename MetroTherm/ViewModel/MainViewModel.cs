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
        public ICommand ShowMessageCommand { get; }


        public ICommand GenerateInvoice {  get; }   



        private void ShowMessage()
        {
            MessageBox.Show(SelectedEquipment.ParameterName);
        }

        public MainViewModel()
        {
            equipmentRepo = new EquipmentRepository(this);
            customerRepo = new CustomerRepository(this);
            invoiceRepository = new InvoiceRepository(this);
            _handler = new DataHandler(null); 
            foreach (Equipment equipment in equipmentRepo.GetAll())
                Equipments.Add(new EquipmentViewModel(equipment));

            foreach (Customer customer in customerRepo.GetAll())
                Customers.Add(new CustomerViewModel(customer));


            ShowMessageCommand = new RelayCommand(
                execute: _ => ShowMessage(),
                canExecute: _ => true
            );

            GenerateInvoice = new RelayCommand(
                execute: _ => SaveInvoice(),
                canExecute: _ => BillingCustomer != null && FromDate != null && ToDate != null
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

        private DateTime _fromDate = DateTime.Today; 
        public DateTime FromDate
        {
            get => _fromDate; 
            set
            {
                _fromDate = value;
                OnPropertyChanged(); 
            }
        }

        private DateTime _toDate = DateTime.Today; 
        public DateTime ToDate
        {
            get => _toDate; 
            set
            {
                _toDate = value; 
                OnPropertyChanged(); 
            }
        }

        private double getPricePerKwh(double Heat_energy_E1 = 0.0, double Cooling_energy_E3 = 0.0, double MC2_Heat_energy_E1 = 0.0)
        {
            double Heat_energy_E1_price = Heat_energy_E1 * PricePerKwh1;

            double Cooling_energy_E3_price = Cooling_energy_E3 * PricePerKwh2;

            double MC2_Heat_energy_E1_price = MC2_Heat_energy_E1 * PricePerKwh3;

            double total_price = Heat_energy_E1_price + Cooling_energy_E3_price + MC2_Heat_energy_E1_price;

            return total_price; 
        }
        private void SaveInvoice()
        {
            var person = BillingCustomer;
            
            double H_E_E1 = equipmentRepo.GetTotalKwh(person, "Heat energy E1", FromDate, ToDate);
            double C_E_E3 = equipmentRepo.GetTotalKwh(person, "Cooling energy E3", FromDate, ToDate);
            double MC2_H_E_E1 = equipmentRepo.GetTotalKwh(person, "MC2 Heat energy E1", FromDate, ToDate);

            double subtotal = getPricePerKwh(H_E_E1, C_E_E3, MC2_H_E_E1);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=================== BØRGE'S VARME ================");
            sb.AppendLine("                       FAKTURA");
            sb.AppendLine(new string('-', 50));

            sb.AppendLine($"Kunde      : {person.Name}");
            sb.AppendLine($"Adresse    : {person.Address}");
            sb.AppendLine(new string('-', 50));

            sb.AppendLine("Beskrivelse: Varmelevering / Energiforbrug");
            sb.AppendLine(new string('-', 50));

            sb.AppendLine($"Subtotal   : {subtotal:F2} kr");
            double moms = subtotal * 0.25;
            sb.AppendLine($"Moms (25%) : {moms:F2} kr");
            sb.AppendLine(new string('-', 50));

            sb.AppendLine($"Total inkl. moms: {(subtotal + moms):F2} kr");
            sb.AppendLine(new string('=', 50));

            _handler.SaveData($"faktura_{person.Name}.txt", sb.ToString());
            MessageBox.Show("Faktura Gemt");
        }
    }
}
