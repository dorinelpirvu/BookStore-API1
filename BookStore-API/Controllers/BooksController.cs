using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.Data;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;


        public BooksController(IBookRepository bookRepository, ILoggerService logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {

            try
            {
                _logger.LogInfo("incepe procedura get all");
                var books = await _bookRepository.FindAll();
                var response = _mapper.Map<List<BookDTO>>(books);
                _logger.LogInfo("s-a efectuat");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {

            try
            {
                _logger.LogInfo("incepe procedura get all");
                var book = await _bookRepository.FindById(id);
                if (book == null)
                {
                    return NotFound();
                }
                var response = _mapper.Map<BookDTO>(book);
                _logger.LogInfo("s-a efectuat");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookDTO)
        {
            try
            {
                if (bookDTO == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookDTO);
                var isSuccess = await _bookRepository.Create(book);
                if (!isSuccess)
                {
                    return InternalError("Book creation failed");
                }
                return Created("Created", new { book });
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message }");
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDTO bookDTO)
        {
            try
            {
                if (bookDTO == null || id < 1 || id != bookDTO.Id)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookDTO);
                var isSuccess = await _bookRepository.Update(book);
                if (!isSuccess)
                {
                    return InternalError("Book creation failed");
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message}- {e.InnerException}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }


                var book = await _bookRepository.FindById(id);
                if (book == null)
                {
                    return BadRequest();
                }
                var isSuccess = await _bookRepository.Delete(book);
                if (!isSuccess)
                {
                    return InternalError("book deletion failed");
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message }-{e.InnerException}");
            }

        }
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "ceva a mers rau");
        }
    }
    
}
