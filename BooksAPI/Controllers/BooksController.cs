using BooksAPI.Exceptions;
using BooksAPI.Models;
using BooksAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private IBooksServices _booksServices;
        public BooksController(IBooksServices booksServices)
        {
            this._booksServices = booksServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<BookModel>> GetBooks(string orderBy = "Id")
        {
            try
            {
                return Ok(_booksServices.GetBooks(orderBy));
            }
            catch (BadRequestOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }
        [HttpGet("{bookId:int}", Name = "GetBook")]
        public ActionResult<BookModel> GetBook(int BookId)
        {
            try
            {
                return Ok(_booksServices.GetBook(BookId));
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<BookModel> CreateBook([FromBody] BookModel bookModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = HttpContext.Request.Host;
                var newBook = _booksServices.CreateBook(bookModel);
                return CreatedAtRoute("GetBook", new { bookId = bookModel.Id }, newBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpDelete("{bookId:int}")]
        public ActionResult<DeleteModel> DeleteBook(int bookId)
        {
            try
            {
                return Ok(_booksServices.DeleteBook(bookId));

            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpPut("{bookId:int}")]
        public IActionResult UpdateBook(int bookId, [FromBody] BookModel booksModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(booksModel.Author) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return Ok(_booksServices.UpdateBook(bookId, booksModel));
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpGet("rating/{rate:int}")]
        public ActionResult<IEnumerable<BookModel>> GetTopBooks(int rate)
        {
            try
            {
                return Ok(_booksServices.GetTopRatedBooks(rate));
            }
            catch (BadRequestOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }
        [HttpGet("fromAuthor/{author}", Name = "GetBooksFromAuthor")]
        public ActionResult<IEnumerable<BookModel>> GetBooksFromAuthor(string author)
        {
            try
            {
                return Ok(_booksServices.GetBooksFromAuthor(author));
            }
            catch (BadRequestOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }
    }
}
