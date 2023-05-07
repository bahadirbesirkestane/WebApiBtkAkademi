

namespace Entities.Exceptions
{
    //abstact olanalar newleme yapılamaz
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message) 
        {
            
        }

    }
}
