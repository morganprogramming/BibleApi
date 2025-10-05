using System.Reflection;

namespace BibleApi.Repository
{
	/// <summary>
	/// This will validate that for each Query name, 
	/// the sql file has a matching query.
	/// This prevents runtime errors of missing queries.
	/// This should be run at startup to validate.
	/// </summary>
	public class QueryValidator
	{
		private readonly ISqlQueryProvider _queryProvider;

		public QueryValidator(ISqlQueryProvider queryProvider)
		{
			_queryProvider = queryProvider;
		}

		public void ValidateQueries<T>() where T : IQuery
		{
			var constants = typeof(T)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Select(x => x.GetValue(null)!.ToString())
				.ToList();

			foreach (var name in constants)
			{
				if (!_queryProvider.GetAll().ContainsKey(name!))
				{
					throw new InvalidOperationException(
						$"Startup check failed: SQL query '{name}' is missing in embedded SQL files.");
				}
			}
		}
	}
}
