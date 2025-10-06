using AutoMapper;
using BibleApi.Models.Bible;
using BibleApi.Profiles;
using BibleApi.Repository;
using BibleApi.Services;
using BibleApi.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace BibleApi.Tests.Services
{
	public class BibleServiceTests
	{
		private readonly MapperConfiguration _mapperConfiguration;

		public BibleServiceTests()
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
		public async Task GetBooks_ShouldReturnDtoBooks()
		{
			// Arrange
			var repositoryMock = new Mock<IRepository>();
			repositoryMock
				.Setup(x => x.GetBooksAsync())
				.ReturnsAsync(BookData.GetBookEntities());

			var bookEntities = BookData.GetBookEntities();

			// Act
			var service = new BibleService(repositoryMock.Object, _mapperConfiguration.CreateMapper());
			var result = await service.GetBooksAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(typeof(List<Models.DTO.Book>), result.GetType());
			Assert.Equal(bookEntities.Count, result.Count);
			Assert.Equal(bookEntities[0].Id, result[0].Id);
			Assert.Equal(bookEntities[0].Name, result[0].Title);

			// Check first book (Old Testament)
			Assert.Equal("Old Testament", result[0].Testament);

			// Check last book (New Testament)
			Assert.Equal("New Testament", result[result.Count - 1].Testament);
		}

		[Fact]
		public async Task GetBook_ShouldReturnDtoBook()
		{
			// Arrange
			var bookEntity = BookData.GetBookEntities().First();
			var repositoryMock = new Mock<IRepository>();
			repositoryMock
				.Setup(x => x.GetBookByIdAsync(bookEntity.Id))
				.ReturnsAsync(bookEntity);

			// Act
			var service = new BibleService(repositoryMock.Object, _mapperConfiguration.CreateMapper());
			var result = await service.GetBookByIdAsync(bookEntity.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(typeof(Models.DTO.Book), result.GetType());
			Assert.Equal(bookEntity.Id, result.Id);
			Assert.Equal(bookEntity.Name, result.Title);
			Assert.Equal("Old Testament", result.Testament);
		}

		[Fact]
		public async Task GetBook_ShouldReturnNull_WhenBookNotExists()
		{
			// Arrange
			var repositoryMock = new Mock<IRepository>();
			repositoryMock
				.Setup(x => x.GetBookByIdAsync(999))
				.ReturnsAsync(null as BookEntity);

			// Act
			var service = new BibleService(repositoryMock.Object, _mapperConfiguration.CreateMapper());
			var result = await service.GetBookByIdAsync(999);

			// Assert
			Assert.Null(result);
		}
	}
}
