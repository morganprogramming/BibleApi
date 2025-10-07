using BibleApi.Models.Bible;

namespace BibleApi.Repository
{
	public interface IRepository
	{
		Task<IEnumerable<BookEntity>> GetBooksAsync();
		Task<BookEntity?> GetBookByIdAsync(int bookId);
		Task<IEnumerable<VerseEntity>> GetChapterVersesAsync(int bookId, int chapter);
	}
}
