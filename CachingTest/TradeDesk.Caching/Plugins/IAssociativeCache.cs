using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Caching.Plugins
{
	/// <summary>
	/// Associative Cache that allows N way or specific item count for items in the cache. This interface should be used
	/// to implement your own custom implementations such as LRU, MRU Random etc.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public interface IAssociativeCache<TKey, TValue> where TValue : class
	{
		int Count { get; }

		void Add(TKey key, TValue value);
		void Add(CacheItem<TKey, TValue> item);

		TValue Get(TKey key);

		void Flush();
	}
}
