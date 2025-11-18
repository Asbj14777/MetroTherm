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
   }
}
