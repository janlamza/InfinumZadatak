using API.Entities;

namespace API.DTOs
{
    public class AddContactDTO
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<AppTelephoneNumber> PhoneNumbers { get; set; }
    }
}