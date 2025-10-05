using BibleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibleApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BibleController : ControllerBase
	{
		private readonly BibleService _bibleService;
		public BibleController(BibleService bibleService)
		{
			_bibleService = bibleService;
		}

		[HttpGet("books")]
		public async Task<IResult> GetBooks()
		{
			var books = await _bibleService.GetBooksAsync();

			return Results.Ok(books);
		}

		[HttpGet("books/{bookId:int}")]
		public async Task<IResult> GetBooks(int bookId)
		{
			var book = await _bibleService.GetBookByIdAsync(bookId);

			if (book == null)
			{
				return Results.NotFound();
			}

			return Results.Ok(book);
		}
	}
}
