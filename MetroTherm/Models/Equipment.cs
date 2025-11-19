using System;

namespace MetroTherm.Models
{
    public class Equipment
    {
        public string DeviceId { get; set; }
        public string SerialNumber { get; set; }
        public string ProductName { get; set; }
        public string ConnectionState { get; set; }
        public string CurrentFwVersion { get; set; }
        public string DesiredFwVersion { get; set; }
        public string ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public string StrVal { get; set; }
        public string ParameterUnit { get; set; }
        public string Timestamp { get; set; }
        public string Category { get; set; }
        public string Writable { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public string StepValue { get; set; }
        public string ScaleValue { get; set; }
        public string ZoneId { get; set; }
        public string SmartHomeCategories { get; set; }
        public string EnumValues { get; set; }

        public Equipment(
            string deviceID, string serialNumber, string productName, string connectionstate,
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