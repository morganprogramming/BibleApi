using BibleApi.Models.Bible;

namespace BibleApi.Repository
{
	public interface IRepository
	{
		Task<IEnumerable<BookEntity>> GetBooks();
	}
}
