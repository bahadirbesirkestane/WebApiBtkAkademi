using AutoMapper;
using Entities.DataTransferObjects;
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
        private readonly IMapper _mapper;
        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books= _manager.Book.GetAllBooks(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
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

        public void UpdateOneBook(int id,
            BookDtoForUpdate bookDto,
            bool trackChanges)
        {
            var entity = _manager
                .Book
                .GetOneBookById(id, trackChanges);

            if (entity == null)
            {
                throw new BookNotFoundExeption(id);
            }

            // checked params
            //

            // Buraya dikkat burayı kendim yorum satırı yaptım.
            //if(book == null)
            //    throw new ArgumentNullException (nameof(book));


            //// burada bir mapping var.atama
            //entity.Title = book.Title;
            //entity.Price = book.Price;

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.Update(entity);
            _manager.Save();

        }
    }
}
