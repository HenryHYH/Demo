using System.Collections.Generic;
using WebApp.Entities;

namespace WebApp.Services
{
    public interface IBookService
    {
        IEnumerable<Book> Get();
    }
}
