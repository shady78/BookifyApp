namespace Bookify.Web.Core.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        //assign default value as not null
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
    }
}
