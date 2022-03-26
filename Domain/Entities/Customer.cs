using System.Collections.Generic;

namespace Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int CustomerId { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
