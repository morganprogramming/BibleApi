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
		private readonly LinkGenerator _linkGenerator;

		public BibleController(BibleService bibleService, IMapper mapper, LinkGenerator linkGenerator)
		{
			_bibleService = bibleService;
			_mapper = mapper;
			_linkGenerator = linkGenerator;
		}

		[HttpGet("books")]
		public async Task<IResult> GetBooks()
		{
			var books = await _bibleService.GetBooksAsync();
			var response = _mapper.Map<List<BookResponse>>(books);

			return Results.Ok(response);
		}

		[HttpGet("books/{bookId:int}")]
		public async Task<IResult> GetBook(int bookId)
		{
			var book = await _bibleService.GetBookByIdAsync(bookId);

			if (book == null)
			{
				return Results.NotFound();
			}
			var response = _mapper.Map<BookResponse>(book);
			return Results.Ok(response);
		}

		[HttpGet("books/{bookId:int}/chapter/{chapterNumber:int}")]
		public async Task<IResult> GetChapter(int bookId, int chapterNumber)
		{
			var chapter = await _bibleService.GetChapterContentAsync(bookId, chapterNumber);

			if (chapter == null)
			{
				return Results.NotFound();
			}

			var response = _mapper.Map<ChapterResponse>(chapter);

			response.Navigation = new ChapterNavigationLinks
			{
				Previous = chapterNumber > 1
			? _linkGenerator.GetPathByAction(HttpContext, nameof(GetChapter), values: new { bookId, chapterNumber = chapterNumber - 1 })
			: null,

				Next = chapterNumber < chapter.BookTotalChapters
			? _linkGenerator.GetPathByAction(HttpContext, nameof(GetChapter), values: new { bookId, chapterNumber = chapterNumber + 1 })
			: null,

				Book = _linkGenerator.GetPathByAction(HttpContext, nameof(GetBook), values: new { bookId })
			};

			return Results.Ok(response);
		}
	}
}
