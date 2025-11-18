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

namespace MetroTherm.ViewModel 
{ 
public class MainViewModel : INotifyPropertyChanged
{
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
        ObservableCollection<Equipment> Equipments = new ObservableCollection<Equipment>();

        public MainViewModel()
        {
            IDataHandler dataHandler = new DataHandler("kundeliste.txt");
            Customers = new ObservableCollection<Customer>(dataHandler.LoadData<Customer>());

            IDataHandler equipmentDataHandler = new DataHandler("myuplink_points_file1_LSC-HL000209-RXpO4.txt");
            Equipments = new ObservableCollection<Equipment>(equipmentDataHandler.LoadData<Equipment>());
            
            foreach (var item in Equipments) MessageBox.Show(item.ParameterName);
            foreach (var item in Customers) MessageBox.Show(item.Name);
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
