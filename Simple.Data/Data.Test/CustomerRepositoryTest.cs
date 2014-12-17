using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using Xunit.Should;

namespace Data.Test
{
    public class CustomerRepositoryTest
    {
        private readonly CustomerRepository repository;

        public CustomerRepositoryTest()
        {
            repository = new CustomerRepository();
        }

        [Theory]
        [InlineData(1, "admin@nop.com", "admin@nop.com")]
        [InlineData(2006, "Henry", "henry@abc.com")]
        public void Test(int id, string expectedUsername, string expectedEmail)
        {
            var customer = repository.Get(id);
            customer.ShouldNotBeNull();
            customer.Email.ShouldBe(expectedEmail);
            customer.Username.ShouldBe(expectedUsername);
        }
    }
}
