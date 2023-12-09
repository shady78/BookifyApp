using Bookify.API.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookify.API.Entities
{

    [Index(nameof(Name), IsUnique = true)]
    public class Author : BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;
        

    }
}
