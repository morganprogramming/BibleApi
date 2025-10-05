namespace BibleApi.Repository
{
	public interface ISqlQueryProvider
	{
		string Get(string name);
		IReadOnlyDictionary<string, string> GetAll();
	}
}
