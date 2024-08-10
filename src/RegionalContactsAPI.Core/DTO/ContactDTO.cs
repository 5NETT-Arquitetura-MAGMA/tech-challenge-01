using System.Collections;
using Newtonsoft.Json;
using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Core.DTO
{
    public class ContactRequestDTO
    {
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public int DDD { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        public List<ValidationError> Validate()
        {
            List<ValidationError> errors = new List<ValidationError>();

            if (string.IsNullOrEmpty(Nome))
            {
                errors.Add(new ValidationError { Field = "Nome", Message = "O campo Nome é obrigatório." });
            }

            if (Telefone <= 0 || Telefone.ToString().Length < 8 || Telefone.ToString().Length > 9)
            {
                errors.Add(new ValidationError { Field = "Telefone", Message = "O campo Telefone deve ter entre 8 e 9 dígitos." });
            }

            if (DDD <= 0 || DDD.ToString().Length != 2)
            {
                errors.Add(new ValidationError { Field = "DDD", Message = "O Campo DDD deve conter 2 números." });
            }

            if (string.IsNullOrEmpty(Email))
            {
                errors.Add(new ValidationError { Field = "Email", Message = "O campo Email é obrigatório." });
            }

            if (string.IsNullOrEmpty(Estado))
            {
                errors.Add(new ValidationError { Field = "Estado", Message = "O campo Estado é obrigatório." });
            }

            if (string.IsNullOrEmpty(Cidade))
            {
                errors.Add(new ValidationError { Field = "Cidade", Message = "O campo Cidade é obrigatório." });
            }

            return errors;
        }
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
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
