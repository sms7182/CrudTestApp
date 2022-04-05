using Mc2.CrudAppTest.Api.Contracts;
using Mc2.CrudAppTest.Api.Data.Configuration;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudAppTest.Api.Services.Commands
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ResponseDto>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GetCustomerQueryHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ResponseDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken = default)
        {
            var customers = await _applicationDbContext.Customers.Where(d => d.Id == request.Id)
            .Select(d => new
            {
                d.BankAccountNumber,
                d.LastName,
                d.FirstName,
                PhoneNumber = d.PhoneNumber,
                d.Email,
                d.DateOfBirth,
                d.Id,
                d.CountryCode
            }).FirstOrDefaultAsync(cancellationToken);
            if (customers is null)
                return new ResponseDto() { IsSuccess = false, Errors = new List<string>() { "Customer not found" } };

            return new ResponseDto()
            {
                IsSuccess = true,
                Data = new List<object> { customers }

            };
        }
    }
}
