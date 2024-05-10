namespace Domain.Entities
{
    public class Usuario(string nome)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; } = nome;
    }
}
