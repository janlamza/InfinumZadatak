using API.Entities;

namespace API.DTOs
{
    public class UpdateContactDTO
    {
         public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<AppTelephoneNumber> PhoneNumbers { get; set; }
    }
}