using AutoMapper;
using Bookify.API.Core.ViewModels;
using Bookify.API.Data;
using Bookify.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AuthorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var authors =  _context.Authors.AsNoTracking().ToList();
            var viewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author = _mapper.Map<Author>(model);
            //author.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.Add(author);
            _context.SaveChanges();

            var viewModel = _mapper.Map<AuthorViewModel>(author);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = _context.Authors.Find(id);

            if (author is null)
                return NotFound();

            var viewModel = _mapper.Map<AuthorFormViewModel>(author);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id ,AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author =  _context.Authors.Find(id);

            if (author is null)
                return NotFound();

            author = _mapper.Map(model, author);
            //author.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            author.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            var viewModel = _mapper.Map<AuthorViewModel>(author);

            return Ok(viewModel);
        }

        [HttpPost("{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var author = _context.Authors.Find(id);

            if (author is null)
                return NotFound();

            author.IsDeleted = !author.IsDeleted;
            //author.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            author.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            return Ok(author.LastUpdatedOn.ToString());
        }

        //public IActionResult AllowItem(AuthorViewModel model)
        //{
        //    var author = _context.Authors.SingleOrDefault(c => c.Name == model.Name);
        //    var isAllowed = author is null || author.Id.Equals(model.Id);

        //    return Ok(isAllowed);
        //}
    }
}
