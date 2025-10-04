using AutoMapper;
using BibleApi.Models.Bible;
using BibleApi.Models.DTO;
using BibleApi.Repository;
using static System.Reflection.Metadata.BlobBuilder;

namespace BibleApi.Services
{
	public class BibleService
	{
		private readonly IMapper _mapper;
		private readonly BibleRepository _repository;

		public BibleService(BibleRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<Book>> GetBooks()
		{
			var booksFromDB = await _repository.GetBooks();
			var books = _mapper.Map<IEnumerable<BookEntity>, List<Book>>(booksFromDB);

			return books;
		}
	}
}
