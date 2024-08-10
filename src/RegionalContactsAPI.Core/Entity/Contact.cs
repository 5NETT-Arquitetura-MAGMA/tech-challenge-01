namespace RegionalContactsAPI.Core.Entity
{
    public class Contact : EntityBase
    {
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public int DDD { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
