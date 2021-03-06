
using Mc2.CrudAppTest.Api.Contracts;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudAppTest.Api.Services.Commands
{
    public class GetCustomerQuery:IRequest<ResponseDto>
    {
        public Guid Id { get; set; }
    }
}
