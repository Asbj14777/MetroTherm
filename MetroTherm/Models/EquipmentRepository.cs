using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTherm.Models
{
    public class EquipmentRepository
    {
        private readonly List<Equipment> _equipment = new(); // gemmer udstyr i hukommelsen

        public EquipmentRepository()     
        {
           
        }

        // Tilføj udstyr
        public void AddEquipment(Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException(nameof(equipment)); 
            _equipment.Add(equipment);
        }

        // Slet udstyr
        public void DeleteEquipment(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return; // ingen handling hvis navnet er tomt
            var existing = _equipment.FirstOrDefault(e => e.ProductName == name); // Find udstyret baseret på navnet
            if (existing != null) // Hvis udstyret findes, slet det
            {
                _equipment.Remove(existing);
            }
        }

        // Opdater udstyr
        public void UpdateEquipment(Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            var index = _equipment.FindIndex(e => e.DeviceId == equipment.DeviceId); 
            if (index >= 0) 
            {
                _equipment[index] = equipment;
            }
        }
        // Hent alt udstyr
        public List<Equipment> GetAll()
        {
            return new List<Equipment>(_equipment);
        }
    }
}
