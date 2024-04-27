namespace Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Usuario(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }
    }
}
