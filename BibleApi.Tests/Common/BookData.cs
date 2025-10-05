using BibleApi.Models.Bible;

namespace BibleApi.Tests.Common
{
	public static class BookData
	{
		public static List<BookEntity> GetBookEntities()
		{
			return new List<BookEntity>()
			{
				new BookEntity()
				{
					Id = 1,
					Name = "Genesis",
					Testament_Reference_Id = 1
				},
				new BookEntity()
				{
					Id = 2,
					Name = "Exodus",
					Testament_Reference_Id = 1
				},
				new BookEntity()
				{
					Id = 40,
					Name = "Matthew",
					Testament_Reference_Id = 2
				},
				new BookEntity()
				{
					Id = 66,
					Name = "Revelation",
					Testament_Reference_Id = 2
				}
			};
		}
	}
}
