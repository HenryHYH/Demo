using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using Dapper.FluentMap.Mapping;

namespace ConsoleApp.Mapping
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            this.Map(x => x.Id).ToColumn("UId");
            this.Map(x => x.Name).ToColumn("UName");
        }
    }
}
