namespace API.Entities
{
    public class AppContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<AppTelephoneNumber> PhoneNumbers { get; set; }

    }
}