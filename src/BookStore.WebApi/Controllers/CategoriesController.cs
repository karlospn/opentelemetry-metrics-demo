using AutoMapper;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApi.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController(IMapper mapper,
        ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryService.GetAll();

            return Ok(mapper.Map<IEnumerable<CategoryResultDto>>(categories));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await categoryService.GetById(id);

            if (category == null) return NotFound();

            return Ok(mapper.Map<CategoryResultDto>(category));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]CategoryAddDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = mapper.Map<Category>(categoryDto);
            var categoryResult = await categoryService.Add(category);

            if (categoryResult == null) return BadRequest();

            return Ok(mapper.Map<CategoryResultDto>(categoryResult));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]CategoryEditDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var categoryResult = await categoryService.Update(mapper.Map<Category>(categoryDto));
            if (categoryResult == null) return BadRequest();

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await categoryService.GetById(id);
            if (category == null) return NotFound();

            var result = await categoryService.Remove(category);

            if (!result) return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("search/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> Search(string category)
        {
            var categories = mapper.Map<List<Category>>(await categoryService.Search(category));

            if (categories == null || categories.Count == 0)
                return NotFound("None category was founded");

            return Ok(categories);
        }
    }
}