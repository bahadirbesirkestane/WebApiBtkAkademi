namespace Entities.DataTransferObjects
{
    public record BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }

    //[Serializable]
    //public record BookDto(int Id, string Title, decimal Price);

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
