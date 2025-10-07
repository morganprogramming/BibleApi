using BibleApi.Models.Bible;

namespace BibleApi.Repository
{
	public interface IRepository
	{
		Task<IEnumerable<BookEntity>> GetBooksAsync();
		Task<BookEntity?> GetBookByIdAsync(int bookId);
		Task<IEnumerable<ChapterEntity>> GetChapterAsync(int bookId, int chapter);
	}
}
