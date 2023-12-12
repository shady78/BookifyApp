using AutoMapper;
using Bookify.API.Data;
using Bookify.API.Entities;
using Bookify.Web.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories;
            var viewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CategoryFormViewModel>(category);

            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(model);

            _context.Add(category);
            _context.SaveChanges();

            var viewModel = _mapper.Map<CategoryViewModel>(category);

            return Ok(viewModel);
        }


        [HttpPut]
        public IActionResult Update(int id, CategoryFormViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var category = GetById(model.Id);
            var category = _context.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            category = _mapper.Map(model, category);

            category.LastUpdatedOn = DateTime.Now;


            _context.Update(category);
            _context.SaveChanges();



            var viewModel = _mapper.Map<CategoryViewModel>(category);

            return Ok(viewModel);
        }

        [HttpPost("{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var category =_context.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }
            category.IsDeleted = !category.IsDeleted;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok(category.LastUpdatedOn.ToString());

            //return category is null ? NotFound() : Ok(category!.LastUpdatedOn.ToString());
        }
    }
}
