using System.Reflection;
using System.Text.RegularExpressions;

namespace BibleApi.Repository
{
	public static class QueryLoader
	{
		private static readonly Dictionary<string, string> _queries = new Dictionary<string, string>();

		static QueryLoader()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceNames = assembly.GetManifestResourceNames()
				.Where(r => r.EndsWith(".sql", StringComparison.OrdinalIgnoreCase));

			foreach (var resourceName in resourceNames)
			{
				using var stream = assembly.GetManifestResourceStream(resourceName);
				using var reader = new StreamReader(stream);
				var content = reader.ReadToEnd();

				// Split by `--name: QueryName marker`
				var matches = Regex.Split(content, @"--\s*name:\s*(\w+)", RegexOptions.IgnoreCase)
					.Where(s => !string.IsNullOrEmpty(s))
					.ToArray();

				// Regex.Split produces [before first match, name1, sql1, name2, sql2,....]
				for (var index = 0; index < matches.Length; index += 2)
				{
					var name = matches[index].Trim();
					var sql = matches[index + 1].Trim();
					_queries.Add(name, sql);
				}
			}
		}

		public static string Get(string name)
		{
			if (_queries.TryGetValue(name, out var sql))
			{
				return sql;
			}

			// TODO:  Custom Exception here????
			throw new KeyNotFoundException($"SQL query '{name}' not found.");
		}
	}
}
