using MyWebAPI.Models;

namespace MyWebAPI.Service
{
	public interface ICacheService : IDisposable
	{
		List<User> GetMData(string cacheKey);
		string GetDData(string cacheKey);
	}
}
