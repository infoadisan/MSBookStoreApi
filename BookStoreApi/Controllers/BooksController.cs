

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApi.Controllers
{
    using Models;
    using Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<List<Book>> Get() =>
             await _bookService.GetAsync();
      

        // GET api/<BooksController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book  = await _bookService.GetAsync(id);

            if(book == null)
            {
                return NotFound();
            }
            return book;
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _bookService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _bookService.GetAsync(id);

            if(book == null)
            {
                return NotFound();
            }
            updatedBook.Id = book.Id;

            await _bookService.UpdateAsync(id,updatedBook);

            return NoContent();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = _bookService.GetAsync(id);

            if(book == null)
            {
                return NotFound();
            }

            await _bookService.RemoveAsync(id);

            return NoContent();
        }
    }
}
