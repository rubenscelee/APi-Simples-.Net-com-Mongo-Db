using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService) =>
            _booksService = booksService;

        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _booksService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>?> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return null;
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult?> Post(Book newBook)
        {
            await _booksService.CreateAsync(newBook);

            return null;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult?> Update(string id, Book updatedBook)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return null;
            }

            updatedBook.Id = book.Id;

            await _booksService.UpdateAsync(id, updatedBook);

            return null;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult?> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return null;
            }

            await _booksService.RemoveAsync(id);

            return null;
        }
        
    }
}