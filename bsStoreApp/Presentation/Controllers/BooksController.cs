using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        // Burası Çok önemli 
        // Bu patterni öğren
        // Çok önemli
        //****************************************
        //private readonly IRepositoryManager _manager;

        //public BooksController(IRepositoryManager manager)
        //{
        //    _manager = manager;
        //}

        ///////////////////////////////////////////////////////////////
        ///

        private readonly IServicesManager _manager;

        public BooksController(IServicesManager manager)
        {
            _manager = manager;
        }

        [HttpGet]

        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager
                    .BookServices.GetAllBooks(false);

                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager
                .BookServices
                .GetOneBookById(id, false);

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


                _manager.BookServices.CreateOneBook(book);

                return StatusCode(200, book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPut("{id:int}")]

        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();
                }

                _manager.BookServices.UpdateOneBook(id, book, true);

                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.BookServices.DeleteOneBook(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //check entity
                var entity = _manager
                    .BookServices
                    .GetOneBookById(id, true);

                if (entity == null)
                {
                    return NotFound();
                }

                bookPatch.ApplyTo(entity);
                _manager.BookServices.UpdateOneBook(id, entity, true);

                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
