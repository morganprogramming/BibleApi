namespace BibleApi.Models.Bible
{
	public class BookEntity
	{
		public int Id { get; set; }
		public int Book_Reference_Id { get; set; }
		public int Testament_Reference_Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int NumberOfChapters { get; set; }
	}
}
