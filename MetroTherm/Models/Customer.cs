using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTherm.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private string _id; 
        private string _name;   
        private string _addresse;   

        public string ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }   


        public string Address
        {
            get { return _addresse; }
            set
            {
                if (_addresse != value)
                {
                    _addresse = value;
                    NotifyPropertyChanged();
                }
            }
        }       



        public Customer(string id, string name, string address)
        {
            _id = id;
            _name = name;
            _addresse = address;
        }       

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
