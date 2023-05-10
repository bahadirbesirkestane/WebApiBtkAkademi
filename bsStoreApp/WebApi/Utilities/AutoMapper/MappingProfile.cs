using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    // burada ": Profile " dedik ve bunun bir automapper- 
    // - profili (class ı) olduğunu söyldek.
    // 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>();
            CreateMap<Book, BookDto>();
        }
    }
}
