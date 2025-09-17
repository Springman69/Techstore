using TechStore.Models;

namespace TechStore.Repositories
{
    public interface IAddressRepository
    {
        List<Address> ReadAddresses();
        public bool ChangeAddress(Address entity);

        public Address GetAddressByClientId(string clientId);

        public bool UpdateAddress(Address address);
    }
}