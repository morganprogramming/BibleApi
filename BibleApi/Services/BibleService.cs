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
	}
}
