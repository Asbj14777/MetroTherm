using System;
using System.ComponentModel;

namespace MetroTherm.Models
{
    public class Equipment : INotifyPropertyChanged
    {
        private string deviceId;
        private string serialNumber;
        private string productName;
        private string connectionState;
        private string currentFwVersion;
        private string desiredFwVersion;
        private string parameterId;
        private string parameterName;
        private string value;
        private string strVal;
        private string parameterUnit;
        private string timestamp;
        private string category;
        private string writable;
        private string minValue;
        private string maxValue;
        private string stepValue;
        private string scaleValue;
        private string zoneId;
        private string smartHomeCategories;
        private string enumValues;

        public string DeviceId
        {
            get => deviceId;
            set
            {
                if (value != deviceId)
                {
                    deviceId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string SerialNumber
        {
            get => serialNumber;
            set
            {
                if (value != serialNumber)
                {
                    serialNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ProductName
        {
            get => productName;
            set
            {
                if (value != productName)
                {
                    productName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ConnectionState
        {
            get => connectionState;
            set
            {
                if (value != connectionState)
                {
                    connectionState = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string CurrentFwVersion
        {
            get => currentFwVersion;
            set
            {
                if (value != currentFwVersion)
                {
                    currentFwVersion = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DesiredFwVersion
        {
            get => desiredFwVersion;
            set
            {
                if (value != desiredFwVersion)
                {
                    desiredFwVersion = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ParameterId
        {
            get => parameterId;
            set
            {
                if (value != parameterId)
                {
                    parameterId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ParameterName
        {
            get => parameterName;
            set
            {
                if (value != parameterName)
                {
                    parameterName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Value
        {
            get => this.value;
            set
            {
                if (value != this.value)
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string StrVal
        {
            get => strVal;
            set
            {
                if (value != strVal)
                {
                    strVal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ParameterUnit
        {
            get => parameterUnit;
            set
            {
                if (value != parameterUnit)
                {
                    parameterUnit = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Timestamp
        {
            get => timestamp;
            set
            {
                if (value != timestamp)
                {
                    timestamp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Category
        {
            get => category;
            set
            {
                if (value != category)
                {
                    category = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Writable
        {
            get => writable;
            set
            {
                if (value != writable)
                {
                    writable = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string MinValue
        {
            get => minValue;
            set
            {
                if (value != minValue)
                {
                    minValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string MaxValue
        {
            get => maxValue;
            set
            {
                if (value != maxValue)
                {
                    maxValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string StepValue
        {
            get => stepValue;
            set
            {
                if (value != stepValue)
                {
                    stepValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ScaleValue
        {
            get => scaleValue;
            set
            {
                if (value != scaleValue)
                {
                    scaleValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ZoneId
        {
            get => zoneId;
            set
            {
                if (value != zoneId)
                {
                    zoneId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string SmartHomeCategories
        {
            get => smartHomeCategories;
            set
            {
                if (value != smartHomeCategories)
                {
                    smartHomeCategories = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string EnumValues
        {
            get => enumValues;
            set
            {
                if (value != enumValues)
                {
                    enumValues = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public Equipment(string deviceID, string serialNumber, string productName, string connectionstate,
                         string currentFwVers, string desiredFWVersion, string paramID, string paramName,
                         string value, string strVal, string paramUnit, string timestamp, string catagory,
                         string writeable, string minVal, string maxVal, string stopVal, string scaleVal,
                         string zoneID, string smarthomeCatagories, string enumValue)
        {
            DeviceId = deviceID;
            SerialNumber = serialNumber;
            ProductName = productName;
            ConnectionState = connectionstate;
            CurrentFwVersion = currentFwVers;
            DesiredFwVersion = desiredFWVersion;
            ParameterId = paramID;
            ParameterName = paramName;
            Value = value;
            StrVal = strVal;
            ParameterUnit = paramUnit;
            Timestamp = timestamp;
            Category = catagory;
            Writable = writeable;
            MinValue = minVal;
            MaxValue = maxVal;
            StepValue = stopVal;
            ScaleValue = scaleVal;
            ZoneId = zoneID;
            SmartHomeCategories = smarthomeCatagories;
            EnumValues = enumValue;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
