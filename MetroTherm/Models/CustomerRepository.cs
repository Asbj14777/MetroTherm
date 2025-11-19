using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MetroTherm.Models
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customers = new(); // gemmer kunder i hukommelsen

        public CustomerRepository()     
        {          
        }

        // Tilføj en kunde
        public void AddCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer)); 
            _customers.Add(customer);
        }

        // Slet en kunde
        public void DeleteCustomer(Customer customer)
        {
            if (customer == null) return;
            var existing = _customers.FirstOrDefault(c => c.ID == customer.ID); 
            if (existing != null)
            {
                _customers.Remove(existing);
            }
           
        }

        // Opdater en kunde
        public void UpdateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            var index = _customers.FindIndex(c => c.ID == customer.ID); // Find kundens indeks
            if (index >= 0) // Hvis kunden findes, opdater den
            {
                _customers[index] = customer;
            }
        }

        // Hent alle kunder
        public List<Customer> GetAll()
        {
            return new List<Customer>(_customers);
        }
    }
}
