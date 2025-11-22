using MetroTherm.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MetroTherm.Models
{
    public class CustomerRepository
    {
        private List<Customer> customers; // gemmer kunder i hukommelsen

        public CustomerRepository(MainViewModel mvm)     
        {
            customers = new List<Customer>();
            InitializeRepository();

        }

        private void InitializeRepository()
        {
            try
            {
                IDataHandler dataHandler = new DataHandler("kundeliste.txt");
                customers = dataHandler.LoadData<Customer>().ToList();
            }
            catch (IOException)
            {
                throw;
            }
        }

        // Tilføj en kunde
        public void AddCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            customers.Add(customer);
        }

        // Slet en kunde
        public void DeleteCustomer(Customer customer)
        {
            if (customer == null) return;
            var existing = customers.FirstOrDefault(c => c.ID == customer.ID); 
            if (existing != null)
            {
                customers.Remove(existing);
            }
           
        }

        // Opdater en kunde
        public void UpdateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            var index = customers.FindIndex(c => c.ID == customer.ID); // Find kundens indeks
            if (index >= 0) // Hvis kunden findes, opdater den
            {
                customers[index] = customer;
            }
        }

        // Hent alle kunder
        public List<Customer> GetAll()
        {
            return customers;
        }
    }
}
