using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using Dapper.FluentMap.Dommel.Mapping;

namespace ConsoleApp.Mapping
{
    public class UserModelMap : DommelEntityMap<UserModel>
    {
        public UserModelMap()
        {
            ToTable("User");
            Map(x => x.Id).ToColumn("UId");
            Map(x => x.Name).ToColumn("UName");
        }
    }
}
