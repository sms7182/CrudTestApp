using FluentValidation;

namespace Mc2.CrudAppTest.Api.Domain
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
         
        }
    }
}
