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

		[HttpGet("books/{bookId:int}/chapter/{chapterId:int}/verses")]
		public async Task<IResult> GetChapters(int bookId, int chapterId)
		{
			var chapter = await _bibleService.GetChapterContentAsync(bookId, chapterId);

			if (chapter == null)
			{
				return Results.NotFound();
			}

			var response = _mapper.Map<ChapterResponse>(chapter);
			// Generate navigation links

			// TODO FIX THIS!!
			//var previousUrl = chapter.ChapterNavigation.PreviousChapter.HasValue
			//	? _linkGenerator.GetUriByAction(HttpContext, action: nameof(GetChapters),
			//	controller: "Bible", values: new { bookId, chapter = chapter.ChapterNavigation.PreviousChapter }) : null;

			//var nextUrl = result.Navigation.NextChapter.HasValue
			//	? _linkGenerator.GetUriByAction(HttpContext, action: nameof(GetChapterAsync),
			//		controller: "Bible", values: new { bookId, chapter = result.Navigation.NextChapter })
			//	: null;
			//response.Navigation.Previous = chapter.ChapterNumber > 1
			//	? _linkGenerator.GetPathByAction(HttpContext, action: nameof(GetChapters), controller: "Bible", values: new { bookId = bookId, chapterId = chapter.ChapterNumber - 1 })
			//	: null;

			return Results.Ok(response);
		}
	}
}
