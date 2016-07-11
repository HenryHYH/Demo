using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Domain.Models;

namespace ConsoleApp.Services.Users
{
    public interface IUserService
    {
        void Insert(User entity);

        IEnumerable<User> Get();
    }
}
