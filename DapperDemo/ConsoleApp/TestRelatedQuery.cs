using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dapper;
using ConsoleApp.Model;

namespace ConsoleApp
{
    public class TestRelatedQuery : BaseTest
    {
        [Fact]
        public void TestOnToN()
        {
            Customer tmpCustomer = null;
            var list = conn.Query<Customer, CustomerAttribute, Customer>(
                                "SELECT * FROM Customer c LEFT JOIN CustomerAttribute ca ON (c.Id = ca.CustomerId) ORDER BY c.Id",
                                (customer, customerAttribute) =>
                                {
                                    if (null == tmpCustomer || tmpCustomer.Id != customer.Id)
                                        tmpCustomer = customer;
                                    if (null != customerAttribute)
                                        tmpCustomer.Attributes.Add(customerAttribute);

                                    return customer;
                                })
                            .ToList();

            Assert.True(list.Count > 0);
            Assert.NotEmpty(list[0].Attributes);
            Assert.NotEmpty(list[1].Attributes);
        }
    }
}
