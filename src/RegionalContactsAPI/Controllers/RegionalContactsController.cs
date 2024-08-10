using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegionalContactsAPI.Core.DTO;
using RegionalContactsAPI.Core.Repository;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionalContactsController(IContactRepository contactRepository, ICacheService cacheService)
        : ControllerBase
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ICacheService _cacheService = cacheService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepository.GetAll();
            var responseDTOs =
                contacts.Select(contact => ContactMapper.MapToResponseDTO(contact))
                    .ToList(); // Map entities to response DTOs
            return Ok(responseDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _contactRepository.Get(id);
            if (contact == null)
            {
                return NotFound();
            }

            var responseDTO = ContactMapper.MapToResponseDTO(contact); // Map entity to response DTO
            return Ok(responseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactRequestDTO requestDTO) // Use the request DTO as the parameter
        {
            var contact = ContactMapper.MapToEntity(requestDTO); // Map request DTO to entity

            //Adicionado para teste de cache
            //var dados = await _cacheService.GetCidades();

            //bool cidadeEstado = dados.Any(d => d.NomeCidade == contact.Cidade.ToUpper() && d.Estado == contact.Estado && d.DDD == contact.DDD.ToString());

            //if (!cidadeEstado)
            //{
            //    // Os dados não correspondem, faça o que for necessário aqui
            //    return BadRequest($"O DDD {contact.DDD} não pertence a cidade de {contact.Cidade} - {contact.Estado}");
            //}

            await _contactRepository.Add(contact);
            var responseDTO = ContactMapper.MapToResponseDTO(contact); // Map entity to response DTO
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, responseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(int id, ContactRequestDTO requestDTO) // Use the request DTO as the parameter
        {
            var contactRequest = await _contactRepository.Get(id);
            var contact = ContactMapper.MapToEntity(requestDTO); // Map request DTO to entity
            if (contactRequest == null)
            {
                return NotFound();
            }

            contactRequest.Nome = contact.Nome;
            contactRequest.Telefone = contact.Telefone;
            contactRequest.DDD = contact.DDD;
            contactRequest.Estado = contact.Estado;
            contactRequest.Cidade = contact.Cidade;

            var dados = await _cacheService.GetCidades();

            await _contactRepository.Update(contactRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.Delete(id);
            return NoContent();
        }
    }
}