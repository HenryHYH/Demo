using Dapper.Extensions.Linq.Core;

namespace ConsoleApp.Domains
{
    public class User : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public bool Enabled { get; set; }
    }
}
