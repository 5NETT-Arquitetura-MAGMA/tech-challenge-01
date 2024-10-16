using Newtonsoft.Json;
using RegionalContactsAPI.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RegionalContactsAPI.Core.DTO
{
    public class ContactRequestDTO
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [JsonProperty("nome")]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Range(100000000, 999999999, ErrorMessage = "O telefone deve ter 9 digitos.")]
        [JsonProperty("telefone")]
        [JsonPropertyName("telefone")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "O DDD é obrigatório.")]
        [Range(01, 99, ErrorMessage = "O DDD deve ter 2 digitos.")]
        [JsonProperty("ddd")]
        [JsonPropertyName("ddd")]
        public int DDD { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O Estado deve conter exatos 2 caracteres. Exemplo: SP")]
        [JsonProperty("estado")]
        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatório.")]
        [JsonProperty("cidade")]
        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }
    }

    public class ContactResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public int DDD { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }

    public static class ContactMapper
    {
        public static ContactResponseDTO MapToResponseDTO(Contact contact)
        {
            return new ContactResponseDTO
            {
                Id = contact.Id,
                Nome = contact.Nome,
                Telefone = contact.Telefone,
                DDD = contact.DDD,
                Email = contact.Email,
                Estado = contact.Estado,
                Cidade = contact.Cidade
            };
        }

        public static Contact MapToEntity(ContactRequestDTO requestDTO)
        {
            return new Contact
            {
                Nome = requestDTO.Nome,
                Telefone = requestDTO.Telefone,
                DDD = requestDTO.DDD,
                Email = requestDTO.Email,
                Estado = requestDTO.Estado,
                Cidade = requestDTO.Cidade
            };
        }
    }
}