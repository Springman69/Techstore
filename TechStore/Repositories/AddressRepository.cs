using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ChangeAddress(Address entity)
        {
            bool isCreated = false;
            _context.Addresses.Add(entity);

            if (_context.SaveChanges() == 1)
            {
                isCreated = true;
            }
            return isCreated;

        }

        public Address GetAddressByClientId(string clientId)
        {
            return _context.Addresses.FirstOrDefault(a => a.ClientId == clientId);
        }

        public bool UpdateAddress(Address address)
        {
            try
            {
                _context.Addresses.Update(address);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Address> ReadAddresses()
        {
            return _context.Addresses?.ToList() ?? new List<Address>();
        }
    }
}