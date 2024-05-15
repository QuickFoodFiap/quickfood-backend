using System.ComponentModel.DataAnnotations;

namespace Core.Domain.DataAnnotations
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute() => ErrorMessage = "{0} é obrigatório.";

        public override bool IsValid(object value)
            => value != null && value is Guid && !Guid.Empty.Equals(value);
    }
}
