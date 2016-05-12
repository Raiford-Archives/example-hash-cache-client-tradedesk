using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TradeDesk.Caching.Plugins
{
	public interface IEvictionPolicy<TKey, TValue> where TValue : class
	{
		TKey RemoveItem(ref LinkedList<CacheItem<TKey, TValue>> list);
		void AddItem(ref LinkedList<CacheItem<TKey, TValue>> list, CacheItem<TKey, TValue> item);
	}
}
