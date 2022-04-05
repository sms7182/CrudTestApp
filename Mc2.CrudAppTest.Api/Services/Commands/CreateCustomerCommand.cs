using Mc2.CrudAppTest.Api.Contracts;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudAppTest.Api.Services.Commands
{
    public class CreateCustomerCommand:IRequest<ResponseDto>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [Required]

        public string Email { get; set; }

        [Required]
        [Display(Name = "Number to Check")]
        public string PhoneNumber { get; set; }
        private string _countryCode;

        [Required]
        [Display(Name = "Issuing Country")]
        public string CountryCode
        {
            get => _countryCode;
            set => _countryCode = value.ToUpperInvariant();
        }
       

        public int BankAccountNumber { get; set; }
        public CreateCustomerCommand()
        {
            Email =String.Empty;
            PhoneNumber =String.Empty;
            LastName =String.Empty;
            FirstName =String.Empty;
        }

    }
}
