using Bookify.API.Core.Consts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookify.API.Core.ViewModels
{
    public class AuthorFormViewModel
    {
        //public int Id { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Author"),
            RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        [Remote(null!, null!, AdditionalFields = "Id", ErrorMessage = Errors.Dublicated)]
        public string Name { get; set; } = null!;
    }
}
