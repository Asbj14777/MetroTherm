using System;

namespace MetroTherm.Models
{
    public class Equipment
    {
        // in viewmodel
        public string DeviceId { get; set; }
        public string ProductName { get; set; }
        public string ConnectionState { get; set; }
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public string Timestamp { get; set; }

        // not in viewmodel read only
        public string SerialNumber { get; }
        public string CurrentFwVersion { get; }
        public string DesiredFwVersion { get; }
        public string ParameterId { get; }
        public string StrVal { get; }
        public string ParameterUnit { get; }
        public string Category { get; }
        public string Writable { get; }
        public string MinValue { get; }
        public string MaxValue { get; }
        public string StepValue { get; }
        public string ScaleValue { get; }
        public string ZoneId { get; }
        public string SmartHomeCategories { get; }
        public string EnumValues { get; }
        public string CustomerId { get; }

        public Equipment
        (
            string deviceID, string serialNumber, string productName, string connectionstate,
            string currentFwVers, string desiredFWVersion, string paramID, string paramName,
            string value, string strVal, string paramUnit, string timestamp, string catagory,
            string writeable, string minVal, string maxVal, string stopVal, string scaleVal,
            string zoneID, string smarthomeCatagories, string enumValue)
        {
            DeviceId = deviceID;
            CustomerId = deviceID;
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

        public override string ToString() =>
             $"DeviceId: {DeviceId}, " +
             $"SerialNumber: {SerialNumber}, " +
             $"ProductName: {ProductName}, " +
            $"ConnectionState: {ConnectionState}, " +
            $"CurrentFwVersion: {CurrentFwVersion}, " +
    $"DesiredFwVersion: {DesiredFwVersion}, " +
    $"ParameterId: {ParameterId}, " +
    $"ParameterName: {ParameterName}, " +
    $"Value: {Value}, " +
    $"StrVal: {StrVal}, " +
    $"ParameterUnit: {ParameterUnit}, " +
    $"Timestamp: {Timestamp}, " +
    $"Category: {Category}, " +
    $"Writable: {Writable}, " +
    $"MinValue: {MinValue}, " +
    $"MaxValue: {MaxValue}, " +
    $"StepValue: {StepValue}, " +
    $"ScaleValue: {ScaleValue}, " +
    $"ZoneId: {ZoneId}, " +
    $"SmartHomeCategories: {SmartHomeCategories}, " +
    $"EnumValues: {EnumValues}";

    }
}
