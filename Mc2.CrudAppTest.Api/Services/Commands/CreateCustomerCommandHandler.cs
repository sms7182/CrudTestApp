using FluentValidation;
using Mc2.CrudAppTest.Api.Contracts;
using Mc2.CrudAppTest.Api.Data.Configuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudAppTest.Api.Services.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseDto>
    {
        private readonly ApplicationDbContext _applicationDbContext;
      
        public CreateCustomerCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
      
        }
        public async Task<ResponseDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = Customer.CreateInstance();
            try
            {
                customer.SetFamilyInformation(request.FirstName, request.LastName);
                customer.SetBankAccountNumber(request.BankAccountNumber);
                customer.SetPhoneNumber(request.PhoneNumber,request.CountryCode);
                customer.SetDateOfBirth(request.DateOfBirth);
                customer.SetEmail(request.Email);
                await _applicationDbContext.Customers.AddAsync(customer, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return new ResponseDto()
                {
                    IsSuccess = true,
                    Id = customer.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }
}
