using System;

namespace MetroTherm.Models
{
    public class Customer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Customer(string id, string name, string address)
        {
            ID = id;
            Name = name;
            Address = address;
        }

        public override string ToString() => $"ID : {ID}, Name: {Name}, Address: {Address}"; 
    }
}
