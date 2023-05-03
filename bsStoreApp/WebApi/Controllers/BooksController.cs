using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Burası Çok önemli 
        // Bu patterni öğren
        // Çok önemli
        //****************************************
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context) 
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();

                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id)
        {
            try
            {
                var book = _context
                .Books
                .Where(x => x.Id == id)
                .SingleOrDefault();

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
  
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest(); //400
                }

                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(200, book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPut("{int:id}")]

        public IActionResult UpdateOneBook([FromRoute(Name ="id")] int id,
            [FromBody]Book book)
        {
            var entity = _context
                .Books
                .Where(x => x.Id == id)
                .SingleOrDefault();

            if(entity == null)
                return NotFound();

            if(id!= book.Id)
                return BadRequest(); //400

            _context.Books.Remove(entity);
            _context.SaveChanges();
        }

    }
}
