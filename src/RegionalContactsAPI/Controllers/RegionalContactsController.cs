using Microsoft.AspNetCore.Mvc;
using RegionalContactsAPI.Core.DTO;
using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionalContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ICacheService _cacheService;

        public RegionalContactsController(IContactService contactService, ICacheService cacheService)
        {
            _contactService = contactService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? ddd)
        {
            IEnumerable<Contact> contacts;

            if (ddd == null)
            {
                contacts = await _contactService.GetAllContactsAsync();
            }
            else
            {
                contacts = await _contactService.GetContactsByDDDAsync(ddd.Value);
            }

            if (contacts == null || !contacts.Any())
            {
                // Retorna 204 se não tem contatos
                return NoContent();
            }

            var responseDTOs = contacts.Select(contact => ContactMapper.MapToResponseDTO(contact)).ToList();
            return Ok(responseDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            var responseDTO = ContactMapper.MapToResponseDTO(contact);
            return Ok(responseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactRequestDTO requestDTO)
        {
            var contact = ContactMapper.MapToEntity(requestDTO);

            //Adicionado para teste de cache
            //var dados = await _cacheService.GetCidades();

            //bool cidadeEstado = dados.Any(d => d.NomeCidade == contact.Cidade.ToUpper() && d.Estado == contact.Estado && d.DDD == contact.DDD.ToString());

            //if (!cidadeEstado)
            //{
            //    // Os dados não correspondem, faça o que for necessário aqui
            //    return BadRequest($"O DDD {contact.DDD} não pertence a cidade de {contact.Cidade} - {contact.Estado}");
            //}

            await _contactService.AddContactAsync(contact);
            var responseDTO = ContactMapper.MapToResponseDTO(contact);
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, responseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContactRequestDTO requestDTO)
        {
            var contactRequest = await _contactService.GetContactByIdAsync(id);
            if (contactRequest == null)
            {
                return NotFound();
            }

            var contact = ContactMapper.MapToEntity(requestDTO);
            contactRequest.Nome = contact.Nome;
            contactRequest.Telefone = contact.Telefone;
            contactRequest.DDD = contact.DDD;
            contactRequest.Estado = contact.Estado;
            contactRequest.Cidade = contact.Cidade;
            contactRequest.Email = contact.Email;

            await _contactService.UpdateContactAsync(contactRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return NoContent();
        }
    }
}