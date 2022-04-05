using Mc2.CrudAppTest.Api.Contracts;
using Mc2.CrudAppTest.Api.Data.Configuration;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudAppTest.Api.Services.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResponseDto>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UpdateCustomerCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ResponseDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await _applicationDbContext.Customers.Where(d => d.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (customer is null)
                return new ResponseDto() { IsSuccess = false, Errors = new List<string>() { "Customer not found" } };
            try
            {
                customer.SetFamilyInformation(request.FirstName, request.LastName);
                customer.SetBankAccountNumber(request.BankAccountNumber);
                customer.SetPhoneNumber(request.PhoneNumber,request.CountryCode);
                customer.SetDateOfBirth(request.DateOfBirth);
                customer.SetEmail(request.Email);

                 _applicationDbContext.Customers.Update(customer);
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
