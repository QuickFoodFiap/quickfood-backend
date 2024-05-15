using System.Diagnostics.CodeAnalysis;

namespace Core.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}