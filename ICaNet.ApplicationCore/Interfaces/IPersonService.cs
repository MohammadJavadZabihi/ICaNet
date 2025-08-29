namespace ICaNet.ApplicationCore.Interfaces
{
    public interface IPersonService
    {
        Task<bool> AddPersonAsync(string name, string phoneNumber, string address,
            string personType, string? emailAddress, string userId);

        Task<bool> DeletePerson(string userId, int personId);
    }
}
