using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public int Index()
        {
            return bookService.Get().Count();
        }
    }
}
