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
    public class AuthorsController:Controller
    {
        private IAuthorsServices _authorsServices;
        public AuthorsController(IAuthorsServices authorsServices)
        {
            this._authorsServices = authorsServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AuthorsModel>> GetAuthors(string orderBy = "Id")
        {
            try
            {
                return Ok(_authorsServices.GetAuthors(orderBy));
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
        [HttpGet("{authorId:int}", Name ="GetAuthor")]
        public ActionResult<AuthorsModel> GetAuthor(int AuthorId)
        {
            try
            {
                return _authorsServices.GetAuthor(AuthorId);
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
        public ActionResult<AuthorsModel> CreateAuthor([FromBody] AuthorsModel authorModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = HttpContext.Request.Host;
                var newAuthor = _authorsServices.CreateAuthor(authorModel);
                return CreatedAtRoute("GetAuthor", new { authorId = authorModel.Id }, newAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpDelete("{authorId:int}")]
        public ActionResult<DeleteModel> DeleteAuthor(int authorId)
        {
            try
            {
                return Ok(_authorsServices.DeleteAuthor(authorId));

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

        [HttpPut("{authorId:int}")]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorsModel authorsModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(authorsModel.Country) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return Ok(_authorsServices.UpdateAuthor(authorId, authorsModel));
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
    }
}
