namespace Bookify.API.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : BaseEntity
    {
        public int Id { get; set; }

        //assign default value as not null
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<BookCategory> Books { get; set; } = new List<BookCategory>();
    }
}
