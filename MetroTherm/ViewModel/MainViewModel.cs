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

namespace MetroTherm.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Equipment> _equipments = new ObservableCollection<Equipment>();
        public ObservableCollection<Equipment> Equipments
        {
            get => _equipments;
            set { _equipments = value; OnPropertyChanged(); }
        }

        private Equipment selectedEquipment;
        public Equipment SelectedEquipment
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
            ShowMessageCommand = new RelayCommand(
                execute: _ => ShowMessage(),
                canExecute: _ => true
            );
            IDataHandler dataHandler = new DataHandler("kundeliste.txt");
            Customers = new ObservableCollection<Customer>(dataHandler.LoadData<Customer>());

            IDataHandler equipmentDataHandler = new DataHandler("myuplink_points_file1_LSC-HL000209-RXpO4.txt");
            Equipments = new ObservableCollection<Equipment>(equipmentDataHandler.LoadData<Equipment>());
        }

        public void showEquipment(Equipment equipment)
        {

        }

        public Equipment chooseEquipment(Equipment equipment)
        {
            return equipment;
        }

        private Equipment _billingEquipment;
        public Equipment BillingEquipment
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
        private Customer _billingCustomer;
        public Customer BillingCustomer
        {
            get => _billingCustomer;
            set
            {
                if (_billingCustomer != value)
                {
                    _billingCustomer = value;
                    OnPropertyChanged();
                    UpdateBillingEquipments();
                }
            }
        }

        private ObservableCollection<Equipment> _billingEquipments = new ObservableCollection<Equipment>();
        public ObservableCollection<Equipment> BillingEquipments
        {
            get => _billingEquipments;
            set { _billingEquipments = value; OnPropertyChanged(); }
        }

        public void UpdateBillingEquipments()
        {
            if (BillingCustomer == null)
            {
                BillingEquipments = new ObservableCollection<Equipment>();
                return;
            }

            string name = BillingCustomer.Name?.Trim();

            string mustContain = name switch
            {
                "Lene Mortensen" => "RXpO4",
                "Anders Poulsen" => "gK6s8",
                _ => null
            };

            IEnumerable<Equipment> filtered;

            if (!string.IsNullOrEmpty(mustContain))
            {
                filtered = Equipments.Where(e =>
                !string.IsNullOrEmpty(e.DeviceId) &&
                e.DeviceId.Contains(mustContain, StringComparison.OrdinalIgnoreCase)).GroupBy(e => e.ParameterName).Select(g => g.First());           

            }
            else
            {
                filtered = Enumerable.Empty<Equipment>();
            }

            BillingEquipments = new ObservableCollection<Equipment>(filtered);

            
            var models = BillingEquipments
                .Select(e => e.ParameterName)
                .Take(3)
                .ToList();

            ModelName1 = models.Count > 0 ? models[0] : "";
            ModelName2 = models.Count > 1 ? models[1] : "";
            ModelName3 = models.Count > 2 ? models[2] : "";


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

    }
}

