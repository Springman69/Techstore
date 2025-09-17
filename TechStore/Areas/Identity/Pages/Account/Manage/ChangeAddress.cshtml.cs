using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ChangeAddressModel : PageModel
    {
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<Client> _userManager;
        private readonly ILogger<ChangeAddressModel> _logger;
        public ChangeAddressModel(IAddressRepository address, ILogger<ChangeAddressModel> logger, UserManager<Client> userManager)
        {
            _addressRepository = address;
            _logger = logger;
            _userManager = userManager;


        }
        [BindProperty]
        public Address Address { get; set; } = new Address();



        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var existingAddress = _addressRepository.GetAddressByClientId(user.Id);

                if (existingAddress != null)
                {
                    Address = existingAddress;
                }
            }
        }

        public async Task OnPost()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Address.ClientId = user.Id.ToString();

                var existingAddress = _addressRepository.GetAddressByClientId(user.Id);

                if (existingAddress != null)
                {
                    existingAddress.Locality = Address.Locality;
                    existingAddress.Street = Address.Street;
                    existingAddress.ApartmentNumber = Address.ApartmentNumber;
                    existingAddress.BuildingNumber = Address.BuildingNumber;
                    existingAddress.PostalCode = Address.PostalCode;
                    existingAddress.ClientId = user.Id.ToString();
                    if (_addressRepository.UpdateAddress(existingAddress))
                    {
                        _logger.LogInformation($"Zaktualizowano adres: {existingAddress.Id}");
                    }
                    else
                    {
                        _logger.LogInformation($"Nie można zaktualizować adresu: {existingAddress.Id}");
                    }
                }
                else
                {
                    if (_addressRepository.ChangeAddress(Address))
                    {
                        _logger.LogInformation($"Dodano adres: {Address.Id}");
                    }
                    else
                    {
                        _logger.LogInformation($"Nie można dodać adresu: {Address.Id}");
                    }
                }
            }
            else
            {
                _logger.LogInformation("Użytkownik nie został znaleziony.");
            }
        }
    }
}
