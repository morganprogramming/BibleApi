using System.Reflection;

namespace BibleApi.Repository
{
	/// <summary>
	/// This will validate that for each Query name, 
	/// the sql file has a matching query.
	/// This prevents runtime errors of missing queries.
	/// This should be run at startup to validate.
	/// </summary>
	public static class QueryValidator
	{
		public static void ValidateBookQueries()
		{
			var constants = typeof(BookQueries)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Select(x => x.GetValue(null)!.ToString())
				.ToList();

			foreach (var name in constants)
			{
				try
				{
					var sql = QueryLoader.Get(name);
				}
				catch (KeyNotFoundException ex)
				{
					throw new InvalidOperationException($"Startup check failed: SQL query '{name}' is missing in embedded SQL files.", ex);
				}
			}
		}
	}
}
