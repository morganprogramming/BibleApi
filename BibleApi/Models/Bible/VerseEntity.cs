namespace BibleApi.Models.Bible
{
	public class VerseEntity
	{
		public int BookId { get; set; }
		public int ChapterNumber { get; set; }
		public int VerseId { get; set; }
		public int VerseNumber { get; set; }
		public string Text { get; set; } = string.Empty;
	}
}
