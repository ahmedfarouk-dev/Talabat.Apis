
namespace Talabat.Core.Entities
{
    public class Address : ModelBase
    {

        public string Fname { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UserApplicationId { get; set; }
        public UserApplication User { get; set; }

    }
}
