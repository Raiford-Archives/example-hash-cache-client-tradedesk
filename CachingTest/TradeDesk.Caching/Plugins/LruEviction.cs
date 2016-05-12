using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Caching.Plugins
{
	public class LruEviction<TKey, TValue> : IEvictionPolicy<TKey, TValue> where TValue : class
	{
		public void AddItem(ref LinkedList<CacheItem<TKey, TValue>> list, CacheItem<TKey, TValue> item)
		{
			LinkedListNode<CacheItem<TKey, TValue>> node = new LinkedListNode<CacheItem<TKey, TValue>>(item);
			list.AddLast(node);
		}

		public TKey RemoveItem(ref LinkedList<CacheItem<TKey, TValue>> list)
		{
			// Remove from LRUPriority
			LinkedListNode<CacheItem<TKey, TValue>> node = list.First;
			list.RemoveFirst();

			return node.Value.Key;
		}


	}
}
