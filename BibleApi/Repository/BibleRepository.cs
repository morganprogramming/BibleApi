using BibleApi.Models.Bible;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Configuration;

namespace BibleApi.Repository
{
	public class BibleRepository : IRepository
	{
		private readonly string _connectionString;
		private readonly ISqlQueryProvider _sqlQueryProvider;

		public BibleRepository(IConfiguration configuration, ISqlQueryProvider sqlQueryProvider)
		{
			_connectionString = GetConnectionString(configuration);
			_sqlQueryProvider = sqlQueryProvider;
		}

		public async Task<IEnumerable<BookEntity>> GetBooksAsync()
		{
			var input = new BookQueryInput();
			var books = await GetBooksAsync(input);

			return books;
		}

		public async Task<BookEntity?> GetBookByIdAsync(int bookId)
		{
			var input = new BookQueryInput()
			{
				BookId = bookId
			};

			var books = await GetBooksAsync(input);
			return books.FirstOrDefault();
		}

		public async Task<IEnumerable<ChapterEntity>> GetChapterAsync(int bookId, int chapter)
		{
			using var connection = new SqliteConnection(_connectionString);
			var baseSql = _sqlQueryProvider.Get(BookQueries.GetBookChapterVerses);

			var verses = await connection.QueryAsync<ChapterEntity>(baseSql, new { bookId = bookId, chapterNumber = chapter });
			return verses;
		}

		private async Task<IEnumerable<BookEntity>> GetBooksAsync(BookQueryInput input)
		{
			using var connection = new SqliteConnection(_connectionString);
			var baseSql = _sqlQueryProvider.Get(BookQueries.GetBooks);

			var builder = new SqlBuilder();
			var template = builder.AddTemplate(baseSql);

			if (input.BookId != null)
			{
				builder.Where("b.id = @bookId", new { bookId = input.BookId });
			}

			var books = await connection.QueryAsync<BookEntity>(template.RawSql, template.Parameters);

			return books;
		}

		private string GetConnectionString(IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("KJVConnection");
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ConfigurationErrorsException("Missing KJVConnection string");
			}

			return connectionString;
		}

		private class BookQueryInput
		{
			public int? BookId { get; set; } = null;
		}
	}
}
