using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using MyWebAPI.DataBase;
using MyWebAPI.Models;

namespace MyWebAPI.Service
{
	public class CacheService : ICacheService
	{
		private bool disposedValue = false;
		private readonly DataContext _context;
		private readonly IMemoryCache _cacheM;
		private readonly IDistributedCache _cacheD;
		private readonly IUserService _userService;

		public CacheService(DataContext context,  IMemoryCache cacheM, IDistributedCache cacheD, IUserService userService)
		{
			_context = context;
			_cacheM = cacheM;
			_cacheD = cacheD;
			_userService = userService;
		}

		public List<User> GetMData(string cacheKey)
		{
			if (!_cacheM.TryGetValue(cacheKey, out List<User> data))
			{
				data = _userService.GetUserList();

				var cacheEntryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
					SlidingExpiration = TimeSpan.FromMinutes(60)
				};

				_cacheM.Set(cacheKey, data, cacheEntryOptions);
			}

			return data;
		}

		public string GetDData(string cacheKey)
		{

			string data = _cacheD.GetString(cacheKey);

			if (data == null)
			{
				data = "Fetched Data";

				var options = new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
					SlidingExpiration = TimeSpan.FromMinutes(60)
				};

				_cacheD.SetStringAsync(cacheKey, data, options);
			}

			return data;
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_context.Dispose();
				}
				disposedValue = true;
			}
		}
		public void Dispose()
		{
			Dispose(true);
		}
	}
}
