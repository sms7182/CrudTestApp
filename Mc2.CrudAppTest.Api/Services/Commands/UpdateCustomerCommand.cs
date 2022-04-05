using Mc2.CrudAppTest.Api.Contracts;

using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudAppTest.Api.Services.Commands
{
    public class UpdateCustomerCommand:IRequest<ResponseDto>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        private string _countryCode;

        [Required]
        [Display(Name = "Issuing Country")]
        public string CountryCode
        {
            get => _countryCode;
            set => _countryCode = value.ToUpperInvariant();
        }

        [EmailAddress]
        [Required]

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int BankAccountNumber { get; set; }
        public UpdateCustomerCommand()
        {
            Email = String.Empty;
            PhoneNumber = String.Empty;
            LastName = String.Empty;
            FirstName = String.Empty;
        }

    }
}
