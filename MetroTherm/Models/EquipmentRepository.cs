using MetroTherm.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MetroTherm.Models
{
    public class EquipmentRepository
    {
        private List<Equipment> equipments; // gemmer udstyr i hukommelsen

        public EquipmentRepository()     
        {
            equipments = new List<Equipment>();
            InitializeRepository();
        }

        private void InitializeRepository()
        {
            try
            {
                IDataHandler dataHandler = new DataHandler("myuplink_points_file1_LSC-HL000209-RXpO4.txt");
                equipments = dataHandler.LoadData<Equipment>().ToList();
            }
            catch (IOException)
            {
                throw;
            }
        }

        // Tilføj udstyr
        public void AddEquipment(Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            equipments.Add(equipment);
        }

        // Slet udstyr
        public void DeleteEquipment(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return; // ingen handling hvis navnet er tomt
            var existing = equipments.FirstOrDefault(e => e.ProductName == name); // Find udstyret baseret på navnet
            if (existing != null) // Hvis udstyret findes, slet det
            {
                equipments.Remove(existing);
            }
        }

        // Opdater udstyr
        public void UpdateEquipment(Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            var index = equipments.FindIndex(e => e.DeviceId == equipment.DeviceId); 
            if (index >= 0) 
            {
                equipments[index] = equipment;
            }
        }
        // Hent alt udstyr
        public List<Equipment> GetAll()
        {
            return equipments;
        }
    }
}
