namespace Entities.Exceptions
{
    // sealed --classınkalıtımı olmayacak
    public sealed class BookNotFoundExeption : NotFoundException
    {
        public BookNotFoundExeption(int id) : 
            base($"The book with id:{id} colud not found.")
        {

        }
    }
}
