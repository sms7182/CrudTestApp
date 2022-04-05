using Mc2.CrudAppTest.Api.Domain;
using System;

namespace Mc2.CrudAppTest.Api
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public int BankAccountNumber { get; private set; }
        public string CountryCode { get;private set; }
        private Customer()
        {
            Id = Guid.NewGuid();
            Email = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
        }

        public static Customer CreateInstance()
        {
            return new Customer();
        }

        public void SetFamilyInformation(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetDateOfBirth(DateTime dateTime)
        {
            DateOfBirth = dateTime.Date;
        }
        public void SetPhoneNumber(string phoneNumber,string countryCode)
        {
            PhoneNumber = phoneNumber;
            CountryCode = countryCode;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetBankAccountNumber(int bankAccountNumber)
        {
            BankAccountNumber = bankAccountNumber;
        }

    }
}