
using Mc2.CrudAppTest.Api.Contracts;
using Mc2.CrudAppTest.Api.Services.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudAppTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class CrudTestController : ControllerBase
    {



        private readonly IMediator _mediator;
        private readonly PhoneNumberUtil _phoneNumberUtil;
        public CrudTestController(IMediator mediator)
        {
            _mediator = mediator;
            _phoneNumberUtil=PhoneNumberUtil.GetInstance();

        }

        [HttpPost(Name = "createCustomer")]
        public async Task<ActionResult<ResponseDto>> CreateCustomer(CreateCustomerCommand createCustomerCommand, CancellationToken cancellationToken = default)
        {
            try
            {
                var phoneNumber = _phoneNumberUtil.Parse(createCustomerCommand.PhoneNumber, createCustomerCommand.CountryCode);
             
                return await _mediator.Send(createCustomerCommand, cancellationToken);
            }
            
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Errors = new List<string>() { ex.Message },
                    IsSuccess = false,
                };
            }
        }

        [HttpPut(Name = "updateCustomer")]
        public async Task<ActionResult<ResponseDto>> UpdateCustomer(UpdateCustomerCommand updateCustomerCommand, CancellationToken cancellationToken = default)
        {
            try
            {
                var phoneNumber = _phoneNumberUtil.Parse(updateCustomerCommand.PhoneNumber, updateCustomerCommand.CountryCode);

                return await _mediator.Send(updateCustomerCommand, cancellationToken);
            }

            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Errors = new List<string>() { ex.Message },
                    IsSuccess = false,
                };
            }

        }

        [HttpDelete(Name = "deleteCustomer")]
        public async Task<ActionResult<ResponseDto>> DeleteCustomer(DeleteCustomerCommand deleteCustomerCommand, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(deleteCustomerCommand, cancellationToken);
        }

        [HttpGet(Name = "getCustomer/{customerId}")]
        public async Task<ActionResult<ResponseDto>> GetCustomers(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetCustomerQuery()
            {
                Id = customerId,
            }, cancellationToken);
        }
    }
}