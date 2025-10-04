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
			var books = await _bibleService.GetBooks();

			return Results.Ok(books);
		}
	}
}
