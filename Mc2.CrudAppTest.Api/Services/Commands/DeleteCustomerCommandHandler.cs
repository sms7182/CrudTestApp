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
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ResponseDto>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DeleteCustomerCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ResponseDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await _applicationDbContext.Customers.Where(d => d.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (customer is null)
                return new ResponseDto() { IsSuccess = false, Errors = new List<string>() { "Customer not found" } };
            _applicationDbContext.Customers.Remove(customer);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new ResponseDto()
            {
                IsSuccess = true,
            };
        }
    }
}
