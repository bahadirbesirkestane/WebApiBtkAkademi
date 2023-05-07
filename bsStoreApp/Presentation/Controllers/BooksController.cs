using Entities.DataTransferObjects;
using Entities.Exceptions;
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

            var books = _manager
                .BookServices.GetAllBooks(false);

            return Ok(books);


 
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            
            var book = _manager
            .BookServices
            .GetOneBookById(id, false);

            

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {

            if (book == null)
            {
                return BadRequest(); //400
            }


            _manager.BookServices.CreateOneBook(book);

            return StatusCode(201, book);

        }

        [HttpPut("{id:int}")]

        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] BookDtoForUpdate bookDto)
        {
            if (bookDto is null)
            {
                return BadRequest();
            }

            _manager.BookServices.UpdateOneBook(id, bookDto, true);

            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {

            _manager.BookServices.DeleteOneBook(id, false);
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            //check entity
            var entity = _manager
                .BookServices
                .GetOneBookById(id, true);


            bookPatch.ApplyTo(entity);
            _manager.BookServices.UpdateOneBook(id,
                new BookDtoForUpdate(entity.Id,entity.Title,entity.Price),
                true);

            return NoContent(); //204 
        }

    }
}
