using BibleApi.Models.Bible;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;

namespace BibleApi.Repository
{
	public class BibleRepository : IRepository
	{
		private readonly string _connectionString;

		public BibleRepository(IConfiguration configuration)
		{
			_connectionString = GetConnectionString(configuration);
		}

		public async Task<IEnumerable<BookEntity>> GetBooks()
		{
			using var connection = new SqliteConnection(_connectionString);
			var sql = QueryLoader.Get(BookQueries.GetBooks);

			var bookList = await connection.QueryAsync<BookEntity>(sql);
			return bookList;
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
	}
}
