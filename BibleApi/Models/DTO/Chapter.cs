namespace BibleApi.Models.DTO
{
	public class Chapter
	{
		public int BookId { get; set; }
		public string BookName { get; set; } = string.Empty;
		public int ChapterNumber { get; set; }
		public int BookTotalChapters { get; set; }
		public List<Verse> Verses { get; set; } = new List<Verse>();
	}
}
