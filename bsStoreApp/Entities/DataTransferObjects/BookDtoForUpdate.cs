using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    //record -- classlar açok benzer ve struct-
    //-gibi referans tiplidir
    public record BookDtoForUpdate(int Id,string Title,decimal Price);

    // veya bu alttaki blok gibi de olur
    // burada init değilde hala set olarak kalırsa readonly olmaz
    // biz readonly olmasını istiyoruz 
    // bir kere atanacak bidaha değiştirilemeyecek.

    //public record BookDtoForUpdate
    //{
    //    public int Id { get; init; }
    //    public string Title { get; init; }
    //    public decimal Price { get; init; }
    //}

    
}
