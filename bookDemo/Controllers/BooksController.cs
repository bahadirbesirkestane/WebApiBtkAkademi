using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {

            var books=ApplicationContext.Books.ToList();

            return Ok(books);

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name ="id")]int id)
        {

            //var books = ApplicationContext.Books.ToList();

            var book =ApplicationContext
                .Books
                .Where (x => x.Id.Equals(id))
                .SingleOrDefault();

            if(book == null)
                return NotFound();

            return Ok(book);

        }


        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest(); //400
                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")]int id,
            [FromBody]Book book)
        {
            // check book -kitap var m yok mı

            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));

            if(entity == null)
            {
                return NotFound(); //404
            }

            // check id
            if(id!=book.Id)
            {
                return BadRequest(); //400
            }

            ApplicationContext.Books.Remove(entity);
            book.Id=entity.Id;
            ApplicationContext.Books.Add(book);

            return Ok(book); 


            
        }

        [HttpDelete]
        public IActionResult DeleteAllBook()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204 

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            var entity=ApplicationContext
                .Books
                .Find(b =>b.Id.Equals(id));

            if (entity == null)
                return NotFound(new
                {
                    statusCode=404,
                    message = $"Book with id:{id} colud not found"
                }) ; //404


            ApplicationContext.Books.Remove(entity);

            return NoContent();

        }


        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            //check entity
            var entity=ApplicationContext
                .Books
                .Find(b =>b.Id.Equals(id));

            if(entity == null)
            {
                return NotFound();
            }

            bookPatch.ApplyTo(entity);
            return NoContent(); //204
        }






    }
}
