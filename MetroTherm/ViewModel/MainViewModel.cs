using MetroTherm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MetroTherm.ViewModel 
{ 
public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
    ObservableCollection<Equipment> Equipments = new ObservableCollection<Equipment>();     
        // ingen constructor-logik = ingen hårdkodet data
        public MainViewModel()
        {
            IDataHandler dataHandler = new DataHandler("CustomerData.tsv"); 
            dataHandler.LoadData(true); // Load customer data   
            Customers = dataHandler.GetCustomerOverview();

            IDataHandler equipmentDataHandler = new DataHandler("EquipmentData.tsv");   
            equipmentDataHandler.LoadData(false);    
            Equipments = equipmentDataHandler.GetEquipmentOverview();
        }
   }
}
