using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
namespace MetroTherm.Models
{
    public interface IDataHandler
    {
        void LoadData(bool IsCustomer);
        void SaveData(string OverViewDataFile);
        ObservableCollection<Equipment> GetEquipmentOverview();
        ObservableCollection<Customer> GetCustomerOverview();
    }

    public class DataHandler : IDataHandler
    {
        // Klasse til at repræsentere et stykke udstyr det blir nok equipmentRepository senere men var lidt nødt til at lave den for at få det til at virke 



        public string DataFileName { get; set; }          // Navn på filen vi læser fra
        private ObservableCollection<Equipment> EquipmentOverview = new ObservableCollection<Equipment>();  // Liste med alt udstyr
        private ObservableCollection<Customer> CustomerOverview = new ObservableCollection<Customer>();     // Liste med alle kunder

        public ObservableCollection<Equipment> GetEquipmentOverview() => EquipmentOverview;     // Hent listen med udstyr ingen ide om vi ville have det på den her møde

        public ObservableCollection<Customer> GetCustomerOverview() => CustomerOverview;        // Hent listen med kunder ingen ide om vi ville have det på den her møde        

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
                        CustomerOverview.Add(new Customer(
                                 c[0],  // customerID
                                 c[1],  // name
                                 c[2]   // address
                                  ));   

                    }
                    else
                    {
                        // Tilføj ny Equipment til listen
                        EquipmentOverview.Add(new Equipment(
                                 c[0],  // deviceID
                                 c[1],  // serialNumber
                                 c[2],  // productName
                                 c[3],  // connectionstate
                                 c[4],  // currentFwVers
                                 c[5],  // desiredFWVersion
                                 c[6],  // paramID
                                 c[7],  // paramName
                                 c[8],  // value
                                 c[9],  // strVal
                                 c[10], // paramUnit
                                 c[11], // timestamp
                                 c[12], // catagory
                                 c[13], // writeable
                                 c[14], // minVal
                                 c[15], // maxVal
                                 c[16], // stopVal
                                 c[17], // scaleVal
                                 c[18], // zoneID
                                 c[19], // smarthomeCatagories
                                 c[20]  // enumValue
                                  ));

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