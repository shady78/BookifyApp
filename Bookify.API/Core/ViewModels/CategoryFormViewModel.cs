﻿using Bookify.API.Core.Consts;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Web.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        //public int Id { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Category")]
        [Remote("AllowItem", null!, AdditionalFields = "Id", ErrorMessage = Errors.Dublicated),
            RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string Name { get; set; } = null!;
    }
}