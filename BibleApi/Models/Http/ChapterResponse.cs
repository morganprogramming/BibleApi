using BibleApi.Models.DTO;

namespace BibleApi.Models.Http
{
	public class ChapterResponse
	{
		public int BookId { get; set; }
		public string BookName { get; set; } = string.Empty;
		public int ChapterNumber { get; set; }
		public int BookTotalChapters { get; set; }
		public List<Verse> Verses { get; set; } = new List<Verse>();
		public ChapterNavigationLinks? Navigation { get; set; }
	}
}
