using AutoMapper;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApi.Dtos.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IMapper mapper,
                                IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();


            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookResultDto>(book));
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]BookAddDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<Book>(bookDto);
            var bookResult = await _bookService.Add(book);

            if (bookResult == null) return BadRequest();

            return Ok(_mapper.Map<BookResultDto>(bookResult));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]BookEditDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var bookResult = await _bookService.Update(_mapper.Map<Book>(bookDto));
            if (bookResult == null) return BadRequest();

            return Ok(bookDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound();

            await _bookService.Remove(book);

            return Ok();
        }

        [HttpGet]
        [Route("search/{bookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.Search(bookName));

            if (books == null || books.Count == 0) return NotFound("None book was founded");

            return Ok(books);
        }

        [HttpGet]
        [Route("search-book-with-category/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.SearchBookWithCategory(searchedValue));

            if (!books.Any()) return NotFound("None book was founded");

            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }
    }
}