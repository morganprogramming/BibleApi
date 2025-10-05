using System.Reflection;
using System.Text.RegularExpressions;

namespace BibleApi.Repository
{
	public class QueryLoader : ISqlQueryProvider
	{
		private readonly Dictionary<string, string> _queries = new Dictionary<string, string>();

		public QueryLoader(Assembly assembly)
		{
			LoadQueries(assembly);
		}

		private void LoadQueries(Assembly assembly)
		{
			var resourceNames = assembly.GetManifestResourceNames()
				.Where(r => r.EndsWith(".sql", StringComparison.OrdinalIgnoreCase));

			foreach (var resourceName in resourceNames)
			{
				using var stream = assembly.GetManifestResourceStream(resourceName);
				using var reader = new StreamReader(stream!);
				var content = reader.ReadToEnd();


				var matches = Regex.Split(content, @"--\s*name:\s*(\w+)", RegexOptions.IgnoreCase)
					.Where(s => !string.IsNullOrEmpty(s))
					.ToArray();

				for (var index = 0; index < matches.Length; index += 2)
				{
					var name = matches[index].Trim();
					var sql = matches[index + 1].Trim();

					if (_queries.ContainsKey(name))
					{
						throw new InvalidOperationException($"Duplicate SQL query name '{name}' found.");
					}

					_queries.Add(name, sql);
				}
			}
		}

		public string Get(string name)
		{
			if (_queries.TryGetValue(name, out var sql))
			{
				return sql;
			}

			throw new KeyNotFoundException($"SQL query '{name}' not found.");
		}

		public IReadOnlyDictionary<string, string> GetAll() => _queries;
	}
}
