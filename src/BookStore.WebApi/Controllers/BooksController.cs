using AutoMapper;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApi.Dtos.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController(IMapper mapper,
        IBookService bookService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookService.GetAll();
            return Ok(mapper.Map<IEnumerable<BookResultDto>>(books));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await bookService.GetById(id);

            if (book == null) return NotFound();

            return Ok(mapper.Map<BookResultDto>(book));
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) return NotFound();

            return Ok(mapper.Map<IEnumerable<BookResultDto>>(books));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]BookAddDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = mapper.Map<Book>(bookDto);
            var bookResult = await bookService.Add(book);

            if (bookResult == null) return BadRequest();

            return Ok(mapper.Map<BookResultDto>(bookResult));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]BookEditDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var bookResult = await bookService.Update(mapper.Map<Book>(bookDto));
            if (bookResult == null) return BadRequest();

            return Ok(bookDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await bookService.GetById(id);
            if (book == null) return NotFound();

            await bookService.Remove(book);

            return Ok();
        }

        [HttpGet]
        [Route("search/{bookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            var books = mapper.Map<List<Book>>(await bookService.Search(bookName));

            if (books == null || books.Count == 0) return NotFound("None book was founded");

            return Ok(books);
        }

        [HttpGet]
        [Route("search-book-with-category/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            var books = mapper.Map<List<Book>>(await bookService.SearchBookWithCategory(searchedValue));

            if (books.Count == 0) return NotFound("None book was founded");

            return Ok(mapper.Map<IEnumerable<BookResultDto>>(books));
        }
    }
}