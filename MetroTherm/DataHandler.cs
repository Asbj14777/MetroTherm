using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
namespace MetroTherm
{
    public interface IDataHandler
    {
        void LoadData(bool IsCustomer);
        void SaveData(string OverViewDataFile);
        List<DataHandler.Equipment> GetEquipmentOverview();
        List<DataHandler.Customer> GetCustomerOverview();
    }

    public class DataHandler : IDataHandler
    {
        // Klasse til at repræsentere et stykke udstyr det blir nok equipmentRepository senere men var lidt nødt til at lave den for at få det til at virke 
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
        }


        public class Customer

        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Addresse { get; set; }

        }

        public string DataFileName { get; set; }          // Navn på filen vi læser fra
        private List<Equipment> EquipmentOverview = new List<Equipment>(); // Liste med alt udstyr
        private List<Customer> CustomerOverview = new List<Customer>(); // Liste med alle kunder

        public List<Equipment> GetEquipmentOverview() => EquipmentOverview;     // Hent listen med udstyr ingen ide om vi ville have det på den her møde

        public List<Customer> GetCustomerOverview() => CustomerOverview;        // Hent listen med kunder ingen ide om vi ville have det på den her møde        

        // Constructor her sætter vi filnavnet
        public DataHandler(string dataFileName) =>
            DataFileName = dataFileName;

        public void LoadData(bool IsCustomerData = false)
        {
            try
            {
                var lines = File.ReadAllLines(DataFileName); // Læs alle linjer
                if (lines.Length <= 1) return;               // Hvis filen kun har header eller er tom, stop

                var header = lines[0].Split('\t');          // Split headeren (ikke brugt, men kunne være til reference)
                string[] c = { };
                // Loop igennem alle linjer (start fra 1, fordi 0 er header)
                for (int i = 1; i < lines.Length; i++)
                {
                    c = lines[i].Split('\t');    // Split hver linje ved ta

                    if (IsCustomerData == true)
                    {
                        // Tilføj ny Customer til listen
                        CustomerOverview.Add(new Customer
                        {
                            ID = c[0],
                            Name = c[1],
                            Addresse = c[2]
                        });

                    }
                    else
                    {
                        // Tilføj ny Equipment til listen
                        EquipmentOverview.Add(new Equipment
                        {
                            DeviceId = c[0],
                            SerialNumber = c[1],
                            ProductName = c[2],
                            ConnectionState = c[3],
                            CurrentFwVersion = c[4],
                            DesiredFwVersion = c[5],
                            ParameterId = c[6],
                            ParameterName = c[7],
                            Value = c[8],
                            StrVal = c[9],
                            ParameterUnit = c[10],
                            Timestamp = c[11],
                            Category = c[12],
                            Writable = c[13],
                            MinValue = c[14],
                            MaxValue = c[15],
                            StepValue = c[16],
                            ScaleValue = c[17],
                            ZoneId = c[18],
                            SmartHomeCategories = c[19],
                            EnumValues = c[20]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}"); // Hvis noget går galt
                return;
            }
        }

        // Gem data til fil ihvertfald sådan jeg forstår det vi ville
        public void SaveData(string OverViewDataFile)
        {
            // Lav header linjen
            var header = string.Join("\t", new[]
            {
                "deviceId","serialNumber","productName","connectionState","currentFwVersion",
                "desiredFwVersion","parameterId","parameterName","value","strVal","parameterUnit",
                "timestamp","category","writable","minValue","maxValue","stepValue","scaleValue",
                "zoneId","smartHomeCategories","enumValues"
             });

            // Lav selve info/data linjerne
            var info = EquipmentOverview.Select(r => string.Join("\t", new[]
            {
                r.DeviceId, r.SerialNumber, r.ProductName, r.ConnectionState, r.CurrentFwVersion,
                r.DesiredFwVersion, r.ParameterId, r.ParameterName, r.Value, r.StrVal,
                r.ParameterUnit, r.Timestamp, r.Category, r.Writable, r.MinValue,
                r.MaxValue, r.StepValue, r.ScaleValue, r.ZoneId, r.SmartHomeCategories,
                r.EnumValues
            }));

            try
            {
                // Hvis filen allerede findes, så slet den først
                if (File.Exists(OverViewDataFile))
                    File.Delete(OverViewDataFile);

                // Concat kombinerer header (første linje) med alle info linjer til en samlet liste som vi kan skrive til filen
                File.WriteAllLines(OverViewDataFile, new[] { header }.Concat(info));

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting existing file: {ex.Message}"); // Hvis noget går galt
                return;
            }
        }
    }
}

/* 
Eksempel af brug
  
        
        DataHandler handler = new DataHandler("myuplink_points_file1_LSC-HL000209-RXpO4.txt");
        handler.LoadData();
        var equipmentList = handler.GetEquipmentOverview(); 
        Console.WriteLine($"Loaded {equipmentList.Count} equipment entries.");      
        Console.WriteLine($"1st entry : DeviceId: {equipmentList[0].DeviceId}, ParameterName: {equipmentList[0].ParameterName}, Value: {equipmentList[0].Value}");
        Console.WriteLine($"2nd entry : DeviceId: {equipmentList[1].DeviceId}, ParameterName: {equipmentList[1].ParameterName}, Value: {equipmentList[1].Value}");
        Console.WriteLine($"3rd entry : DeviceId: {equipmentList[2].DeviceId}, ParameterName: {equipmentList[2].ParameterName}, Value: {equipmentList[2].Value}");
        handler.SaveData("output.txt");     

*/