namespace ICaNer.Shared.DTOs.Pepole
{
    public class AddPersonRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }

        public string? EmailAddress { get; set; }
    }
}
