using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLinqToObjects
{
    class Customer
    {
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public string City { get; set; }
    }
    class Program
    {
        private static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {ContactName = "Maria Anders", City = "Berlin"},
                new Customer {ContactName = "Ana Trujillo", City = "Mexico D.F."},
                new Customer {ContactName = "Antonio Moreno", City = "Mexico D.F."}
            };
        }
        public static void Main(string[] args)
        {
            var query = from customer
                        in GetCustomers()
                        where customer.City == "Mexico D.F."
                        select new { City = customer.City, ContactName = customer.ContactName }; //This lines creates an anonymous type behind scenes. Creating a very simple class at compile time;

            foreach (var cityAndContact in query)
            {
                Console.WriteLine(cityAndContact);
            }
        }
    }
}
