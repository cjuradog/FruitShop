using System.ComponentModel.DataAnnotations;

namespace FruitShop.V1.Controllers.Customers.Request
{
    public class CustomerRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Dni { get; set; }
    }
}
