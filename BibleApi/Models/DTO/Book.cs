namespace BibleApi.Models.DTO
{
	public record Book
	{
		public int Id { get; set; }
		public string Testament { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
	}
}
