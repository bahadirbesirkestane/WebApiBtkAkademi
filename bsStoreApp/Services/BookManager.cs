using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookServices
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();

            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            // check entity

            var entity = _manager
                .Book
                .GetOneBookById(id, trackChanges);
            if(entity == null) 
            {
                throw new BookNotFoundExeption(id);
            }

            _manager.Book.DeleteOneBook(entity);
            _manager .Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book= _manager.Book.GetOneBookById (id, trackChanges);

            if(book == null)
            {
                throw new BookNotFoundExeption(id);

            }
            return book;
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager
                .Book
                .GetOneBookById(id, trackChanges);

            if (entity == null)
            {
                throw new BookNotFoundExeption(id);
            }

            // checked params

            if(book == null)
                throw new ArgumentNullException (nameof(book));

            entity.Title = book.Title;
            entity.Price = book.Price;

            _manager.Book.Update(entity);
            _manager.Save();

        }
    }
}
