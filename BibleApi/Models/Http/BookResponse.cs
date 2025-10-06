namespace BibleApi.Models.Http
{
	public class BookResponse
	{
		public int Id { get; set; }
		public string Testament { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public int NumberOfChapters { get; set; }
	}
}
