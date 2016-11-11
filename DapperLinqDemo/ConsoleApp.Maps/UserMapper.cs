using ConsoleApp.Domains;
using Dapper.Extensions.Linq.Mapper;

namespace ConsoleApp.Maps
{
    public class UserMapper : AutoClassMapper<User>
    {
        public UserMapper()
        {
            Table("User");
        }
    }
}
