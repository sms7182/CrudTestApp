using FluentValidation.TestHelper;
using Mc2.CrudAppTest.Api;
using Mc2.CrudAppTest.Api.Data.Configuration;
using Mc2.CrudAppTest.Api.Domain;
using Mc2.CrudAppTest.Api.Services.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests
{
    public class BddTddTests : IClassFixture<WebApplicationFactory<Startup>>
    {
       
        private readonly ApplicationDbContext _applicationDbContext;
        public BddTddTests(WebApplicationFactory<Startup> factory)
        {
          
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "CrudTestDB").Options;

            _applicationDbContext = new ApplicationDbContext(options);


        }

        [Fact]
        public async Task CreateCustomerValid_ReturnsSuccess()
        {
            var email = "sms7182@gmail.com";
            var birth = new DateTime(1983, 11, 19);
            var bankAccount = 123;
            var firstName = "Mojtaba";
            var lastName = "Safavi";
            var phoneNumber = "9123794709";

          

            var cr = new CreateCustomerCommandHandler(_applicationDbContext);


            var result = await cr.Handle(new CreateCustomerCommand()
            {
                BankAccountNumber = bankAccount,
                DateOfBirth = birth,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                CountryCode="ir"
            });
            Assert.True(result.IsSuccess);
            var customer = await _applicationDbContext.Customers.Where(y => y.Id == result.Id).FirstOrDefaultAsync();
            Assert.NotNull(customer);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(bankAccount, customer.BankAccountNumber);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phoneNumber, customer.PhoneNumber);
            Assert.Equal(birth, customer.DateOfBirth);



        }
        [Fact]
        public async Task UpdateCustomerValid_ReturnSuccess()
        {
            var email = "sms7182@gmail.com";
            var birth = new DateTime(1983, 11, 19);
            var bankAccount = 123;
            var firstName = "Mojtaba";
            var lastName = "Safavi";
            var phoneNumber = "9123794709";



            var customerCreated=Customer.CreateInstance();
            customerCreated.SetBankAccountNumber(75);
            customerCreated.SetPhoneNumber("9123794777","ir");
            customerCreated.SetEmail("m@gmail.com");
            customerCreated.SetDateOfBirth(new DateTime(2022, 11, 19));
            customerCreated.SetFamilyInformation("Noname", "NoLastName");
           await _applicationDbContext.Customers.AddAsync(customerCreated);
           await _applicationDbContext.SaveChangesAsync();

            var cr = new UpdateCustomerCommandHandler(_applicationDbContext);


            var result = await cr.Handle(new UpdateCustomerCommand()
            {
                Id=customerCreated.Id,
                BankAccountNumber = bankAccount,
                DateOfBirth = birth,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                CountryCode="ir"
            });
            Assert.True(result.IsSuccess);
            var customer = await _applicationDbContext.Customers.Where(y => y.Id == result.Id).FirstOrDefaultAsync();
            Assert.NotNull(customer);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(bankAccount, customer.BankAccountNumber);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phoneNumber, customer.PhoneNumber);
            Assert.Equal(birth, customer.DateOfBirth);
        }

        [Fact]
        public async Task CreateCustomerInValid_Exception()
        {
            var customer = Customer.CreateInstance();
            customer.SetBankAccountNumber(75);
            customer.SetPhoneNumber("9123794777", "ir");
            customer.SetEmail("gmail.com");
            customer.SetDateOfBirth(new DateTime(2022, 11, 19));
            customer.SetFamilyInformation("Noname", "NoLastName");

            var customerValidator=new CustomerValidator();
           var result= customerValidator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(d => d.Email);

         


        }

        [Fact]
        public async Task GetCustomerValid_ReturnSuccess()
        {
            var email = "sms@gmail.com";
            var birth = new DateTime(1983, 11, 19);
            var bankAccount = 123;
            var firstName = "fatalError";
            var lastName = "Sa";
            var phoneNumber = "9123794709";



            var cr = new CreateCustomerCommandHandler(_applicationDbContext);


            var result = await cr.Handle(new CreateCustomerCommand()
            {
                BankAccountNumber = bankAccount,
                DateOfBirth = birth,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                CountryCode = "ir"
            });
            Assert.True(result.IsSuccess);

            var query = new GetCustomerQueryHandler(_applicationDbContext);


            var queryResult = await query.Handle(new GetCustomerQuery()
            {
                Id=result.Id.Value
           });

          
            Assert.NotNull(queryResult);
            Assert.True(queryResult.IsSuccess);
            Assert.NotEmpty(queryResult.Data);
            object customer = queryResult.Data[0];
           Assert.Equal(email, customer.GetType().GetProperty("Email").GetValue(customer));
            Assert.Equal(phoneNumber, customer.GetType().GetProperty("PhoneNumber").GetValue(customer));



        }

        [Fact]
        public async Task DeleteCustomerValid_ReturnSuccess()
        {
           var customerCreated = Customer.CreateInstance();
            customerCreated.SetBankAccountNumber(75);
            customerCreated.SetPhoneNumber("9123794777", "ir");
            customerCreated.SetEmail("m@gmail.com");
            customerCreated.SetDateOfBirth(new DateTime(2022, 11, 19));
            customerCreated.SetFamilyInformation("Noname", "NoLastName");
            await _applicationDbContext.Customers.AddAsync(customerCreated);
            await _applicationDbContext.SaveChangesAsync();

            var cr = new DeleteCustomerCommandHandler(_applicationDbContext);


            var result = await cr.Handle(new DeleteCustomerCommand()
            {
                Id = customerCreated.Id,
                
            });
            Assert.True(result.IsSuccess);
            var customer = await _applicationDbContext.Customers.Where(y => y.Id == result.Id).FirstOrDefaultAsync();
            Assert.Null(customer);
          
        }


    }
}
