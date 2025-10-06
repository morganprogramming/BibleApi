using AutoMapper;
using BibleApi.Models.Http;
using BibleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibleApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BibleController : ControllerBase
	{
		private readonly BibleService _bibleService;
		private readonly IMapper _mapper;
		public BibleController(BibleService bibleService, IMapper mapper)
		{
			_bibleService = bibleService;
			_mapper = mapper;
		}

		[HttpGet("books")]
		public async Task<IResult> GetBooks()
		{
			var books = await _bibleService.GetBooksAsync();
			var response = _mapper.Map<List<BookResponse>>(books);

			return Results.Ok(response);
		}

		[HttpGet("books/{bookId:int}")]
		public async Task<IResult> GetBooks(int bookId)
		{
			var book = await _bibleService.GetBookByIdAsync(bookId);

			if (book == null)
			{
				return Results.NotFound();
			}
			var response = _mapper.Map<BookResponse>(book);
			return Results.Ok(response);
		}
	}
}
