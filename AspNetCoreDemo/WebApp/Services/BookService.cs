using System.Collections.Generic;
using WebApp.Entities;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class BookService : IBookService
    {
        private IRepository<Book> repository;

        public BookService(IRepository<Book> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Book> Get()
        {
            return repository.Get();
        }
    }
}
