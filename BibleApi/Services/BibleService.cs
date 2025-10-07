using AutoMapper;
using BibleApi.Models.Bible;
using BibleApi.Models.DTO;
using BibleApi.Repository;

namespace BibleApi.Services
{
	public class BibleService
	{
		private readonly IMapper _mapper;
		private readonly IRepository _repository;

		public BibleService(IRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<Book>> GetBooksAsync()
		{
			var booksFromDB = await _repository.GetBooksAsync();
			var books = _mapper.Map<IEnumerable<BookEntity>, List<Book>>(booksFromDB);

			return books;
		}

		public async Task<Book?> GetBookByIdAsync(int bookId)
		{
			var bookFromDB = await _repository.GetBookByIdAsync(bookId);
			if (bookFromDB == null)
			{
				return null;
			}

			var book = _mapper.Map<BookEntity, Book>(bookFromDB);
			return book;	
		}

		public async Task<Chapter?> GetChapterContentAsync(int bookId, int chapterNumber)
		{
			var book = await GetBookByIdAsync(bookId);
			if (book == null)
			{
				return null;
			}

			var versesFromDB = await _repository.GetChapterAsync(bookId, chapterNumber);
			var verses = _mapper.Map<IEnumerable<ChapterEntity>, List<Verse>>(versesFromDB);

			if (verses == null || !verses.Any())
			{
				return null;
			}

			var chapter = new Chapter()
			{
				BookId = book.Id,
				BookName = book.Title,
				ChapterNumber = chapterNumber,
				BookTotalChapters = book.NumberOfChapters,
				Verses = verses
			};

			return chapter;
		}
	}
}
