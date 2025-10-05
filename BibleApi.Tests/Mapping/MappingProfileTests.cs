using AutoMapper;
using BibleApi.Models.Bible;
using BibleApi.Models.DTO;
using BibleApi.Profiles;
using BibleApi.Services;
using BibleApi.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace BibleApi.Tests.Mapping
{
	public class MappingProfileTests
	{
		private readonly MapperConfiguration _mapperConfiguration;

		public MappingProfileTests()
		{
			var iLoggerMock = new Mock<ILogger<BibleService>>();
			var loggerFactoryMock = new Mock<ILoggerFactory>();
			loggerFactoryMock
				.Setup(f => f.CreateLogger(It.IsAny<string>()))
				.Returns(iLoggerMock.Object);

			var configExpression = new MapperConfigurationExpression();
			configExpression.AddProfile<BibleProfile>();
			_mapperConfiguration = new MapperConfiguration(configExpression, loggerFactoryMock.Object);
		}

		[Fact]
		public void BiblProfile_BookEntityToBook_OldTestament_Singular()
		{
			// Arrange
			var entity = BookData.GetBookEntities().Where(x => x.Testament_Reference_Id == 1).First();

			// Act
			var mappedBook = _mapperConfiguration.CreateMapper().Map<Book>(entity);

			// Assert
			Assert.NotNull(mappedBook);
			Assert.Equal(entity.Id, mappedBook.Id);
			Assert.Equal(entity.Name, mappedBook.Title);
			Assert.Equal("Old Testament", mappedBook.Testament);
		}

		[Fact]
		public void BiblProfile_BookEntityToBook_NewTestament_Singular()
		{
			// Arrange
			var entity = BookData.GetBookEntities().Where(x => x.Testament_Reference_Id == 2).First();

			// Act
			var mappedBook = _mapperConfiguration.CreateMapper().Map<Book>(entity);

			// Assert
			Assert.NotNull(mappedBook);
			Assert.Equal(entity.Id, mappedBook.Id);
			Assert.Equal(entity.Name, mappedBook.Title);
			Assert.Equal("New Testament", mappedBook.Testament);
		}

		[Fact]
		public void BiblProfile_BookEntityToBook_List()
		{
			// Arrange
			var entities = BookData.GetBookEntities();

			// Act
			var mappedBooks = _mapperConfiguration.CreateMapper().Map<List<Book>>(entities);

			// Assert
			Assert.NotNull(mappedBooks);
			Assert.Equal(entities.Count, mappedBooks.Count);
		}
	}
}
