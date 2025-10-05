using BibleApi.Repository;
using Moq;

namespace BibleApi.Tests.RepositoryTests
{
	public class QueryValidatorTests
	{
		[Fact]
		public void ValidateQueries_AllQueriesExist_ShouldNotThrowException()
		{
			// Arrange
			var mockProvider = new Mock<ISqlQueryProvider>();
			mockProvider
				.Setup(p => p.GetAll())
				.Returns(new Dictionary<string, string> { 
					{ "TestQuery_1", "SELECT 1" } ,
					{ "TestQuery_2", "SELECT 2" }
				});

			// Act
			var validator = new QueryValidator(mockProvider.Object);

			// Assert
			//  Note:  It will throw an error if a query is missing, which will fail the test.  No Assert necessary.
			validator.ValidateQueries<TestQueries>();
		}

		[Fact]
		public void ValidateQueries_ShouldThrowException_WhenQueryMissing()
		{
			// Arrange
			var mockProvider = new Mock<ISqlQueryProvider>();
			mockProvider
				.Setup(p => p.GetAll())
				.Returns(new Dictionary<string, string> { { "ExistingQuery", "SELECT 1" } });

			// Act
			var validator = new QueryValidator(mockProvider.Object);

			// Assert
			Assert.Throws<InvalidOperationException>(() => validator.ValidateQueries<TestQueries>());
		}
	}
}
