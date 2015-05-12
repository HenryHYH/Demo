using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;

namespace FW.Data.Tests
{
    public class SchemaTest
    {
        [Fact]
        public void Can_generate_schema()
        {
            Database.SetInitializer<NopObjectContext>(null);
            var ctx = new NopObjectContext("Test");
            string result = ctx.CreateDatabaseScript();
            result.Should().NotBeNull();
            Console.Write(result);
        }
    }
}
