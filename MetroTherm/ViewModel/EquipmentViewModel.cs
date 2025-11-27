using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroTherm.Models;

namespace MetroTherm.ViewModel
{
    public class EquipmentViewModel : BaseViewModel
    {
        private readonly Equipment _equipment;

        // equipment properties exposed to view
        public string DeviceId
        {
            get { return _equipment.DeviceId; }
            set {  _equipment.DeviceId = value; OnPropertyChanged(); }
        }

        public string ProductName
        {
            get { return _equipment.ProductName; }
            set { _equipment.ProductName = value; OnPropertyChanged(); }
        }

        public string ConnectionState
        {
            get { return _equipment.ConnectionState; }
            set { _equipment.ConnectionState = value; OnPropertyChanged(); }
        }

        public string ParameterName
        {
            get { return _equipment.ParameterName; }
            set { _equipment.ParameterName = value; OnPropertyChanged(); }
        }

        public string Value
        {
            get { return _equipment.Value; }
            set { _equipment.Value = value; OnPropertyChanged(); }
        }

        public string Timestamp
        {
            get 
            {
                DateTime val = DateTime.Parse(_equipment.Timestamp);
                return $"{val:dd-MM-yyyy}";  
            }
            set { _equipment.Timestamp = value; OnPropertyChanged(); }
        }

        public EquipmentViewModel(Equipment equipment)
        {
            _equipment = equipment;
        }
    }
}
