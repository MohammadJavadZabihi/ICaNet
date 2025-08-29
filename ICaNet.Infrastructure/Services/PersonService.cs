using ICaNet.ApplicationCore.Entities.Pepole;
using ICaNet.ApplicationCore.Exceptions;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ICaNet.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly CoreDbContext _context;
        public PersonService(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPersonAsync(string name, string phoneNumber, string address, 
            string personType, string? emailAddress, string userId)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => 
            p.Name == name && p.PhoneNumber == phoneNumber);

            if (person != null)
            {
                throw new CustomeException("شما قبلا فردی با این مشخصات ثبت کرده‌ایید");
            }

            var newPerson = new Person
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Name = name,
                EmailAddress = emailAddress,
                UserId = userId
            };

            await _context.People.AddAsync(newPerson);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePerson(string userId, int personId)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => 
            p.UserId == userId && p.Id == personId);

            if (person == null)
            {
                throw new CustomeException("فردی با این مشخصات وجود ندارد");
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
