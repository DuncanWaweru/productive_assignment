using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Productive.Server.Models;
using Productive.Server.Repositories;
using Productive.Server.Repositories.Interfaces;
using Productive.Server.Services;
using Productive.Shared.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Productive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;
        private readonly IClientCellPhoneRepository _clientCellPhoneRepository;

        public ClientsController(IMapper mapper, IClientRepository clientRepository, IClientCellPhoneRepository clientCellPhoneRepository)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _clientCellPhoneRepository = clientCellPhoneRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientRepository.GetAsync();

            var clientsVm = _mapper.Map<IEnumerable<ClientViewModel>>(clients);

            return Ok(clientsVm);
        }

       

        [HttpGet("{id}")]

        public async Task<IActionResult> GetClient([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var clientId))
                return BadRequest("Invalid id");

            var client = await _clientRepository.GetAsync(clientId);

            var clientVm = _mapper.Map<ClientViewModel>(client);

            return Ok(clientVm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateViewModel client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (client == null)
                    return BadRequest($"{nameof(client)} cannot be null");
                //validate phonenumber
                (bool status,string result)  = CellPhoneNumberService.Validate(client.CellPhone);
                if (!status)
                {
                    return BadRequest(new { error = result});
                }

                //check if the email has been taken
                if (await _clientRepository.CheckByEmail(client.Email))
                {
                    return BadRequest(new { error = $"That email has already been used" });
                }
                //check if phone number exist if not create it as active.

                Models.ClientCellPhone clientCellPhone = await _clientCellPhoneRepository.CheckByPhoneNumberAsync(result);

                if (clientCellPhone == null)
                {
                    //create the cell phone
                    clientCellPhone = new Models.ClientCellPhone {

                    CellPhone =result,
                    CellPhoneStatus = Core.Enums.EmailCellPhoneStatus.Active

                    };
                    var (phoneCreatedSuccess, phoneError) = await _clientCellPhoneRepository.CreateAsync(clientCellPhone);
                    if (!phoneCreatedSuccess)
                    {
                        return BadRequest(new { error = $"Unable to create client : {phoneError}!"});
                    }
                }
                else
                {
                    //assign the cell phone
                }

                var appClient = _mapper.Map<Models.Client>(client);
                appClient.ClientCellPhoneId = clientCellPhone.Id; //at this point rest assured we have a phone Id
                var (success, error) = await _clientRepository.CreateAsync(appClient);
                if (!success)
                    return BadRequest(error);

                var clientVm = await GetClientViewModelHelper(appClient.Id);
                return Ok(clientVm);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.InnerException?.Message.ToString());

            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] string id, [FromBody] ClientViewModel client)
        {
           // var client = _mapper.Map<ClientEditViewModel>(clientVm);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (client == null)
                return BadRequest($"{nameof(client)} cannot be null");

            if (!Guid.TryParse(id, out var clientId))
                return BadRequest("Invalid id");

            if (clientId != client.Id)
                return BadRequest(new { error = "Conflicting id in parameter and model data" });

            var appClient = await _clientRepository.GetAsync(clientId);
            //check if PhoneNumber has been updated

            //remove the previous relationship to pave way for the new one
           

            if (appClient.ClientCellPhone.CellPhone != client.ClientCellPhone.CellPhone)
            {
                //Phone number has been updated, we need to check if the new number exist,...
                //if it does we have to adopt the status of the existing number.
                //Then the status can be updated explicitely.
                var phoneNumber = await _clientCellPhoneRepository.CheckByPhoneNumberAsync(client.ClientCellPhone.CellPhone);
                if (phoneNumber == null)
                {
                    //create the new PhoneNumber
                    phoneNumber = new Models.ClientCellPhone
                    {

                        CellPhone = client.ClientCellPhone.CellPhone,
                        CellPhoneStatus = Core.Enums.EmailCellPhoneStatus.Active

                    };
                    var (phoneCreatedSuccess, phoneError) = await _clientCellPhoneRepository.CreateAsync(phoneNumber);
                    if (!phoneCreatedSuccess)
                    {
                        return BadRequest(new { error = $"Unable to update client : {phoneError}!" });
                    }
                }
                var data = await _clientRepository.FindAsync(clientId);
                appClient.ClientCellPhone.Clients.Remove(data);
          bool resltStatus=      await _clientRepository.RemoveRelationShipAsync(clientId, phoneNumber.Id);

                client.ClientCellPhoneId = phoneNumber.Id;
                client.ClientCellPhone = _mapper.Map<ClientCellPhoneViewModelWithoutLoop>(phoneNumber);
            }

            if (appClient == null)
                return NotFound();

            _mapper.Map(client, appClient);

            var (success, error) = await _clientRepository.UpdateAsync(appClient);
            if (!success)
                return BadRequest(error);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var clientId))
                return BadRequest("Invalid id");

            var appClient = await _clientRepository.GetAsync(clientId);

            if (appClient == null)
                return NotFound(new {error ="You cannot delete that client!"});

            var clientVm = await GetClientViewModelHelper(appClient.Id);

            var (success, error) = await _clientRepository.DeleteAsync(appClient);
            if (!success)
                return BadRequest(new { error = error });

            return Ok(clientVm);
        }

        private async Task<ClientViewModel> GetClientViewModelHelper(Guid id)
        {
            var client = await _clientRepository.GetAsync(id);
            return client != null ? _mapper.Map<ClientViewModel>(client) : null;
        }
        private string GetUserName()
        {
            if (User.Identity?.Name != null)
            {
                return User.Identity?.Name;
            }
            var user = User.Claims.Where(x => x.Type == "name").FirstOrDefault();
            if (user != null)
            {
                return user.Value;
            }
            return null;
        }
    }
}