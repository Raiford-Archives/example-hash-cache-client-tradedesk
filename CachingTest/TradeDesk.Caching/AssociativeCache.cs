using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TradeDesk.Caching;
using TradeDesk.Caching.Plugins;

namespace TradeDesk.Caching
{
	/// <summary>
	/// Lru implementation for an Associative Cache
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class AssociativeCache<TKey, TValue> : IAssociativeCache<TKey, TValue> where TValue : class
	{
		#region Private Fields
		private readonly IEvictionPolicy<TKey, TValue> _evictionPolicy = null;
		private readonly Dictionary<TKey, CacheItem<TKey, TValue>> _items;
		private LinkedList<CacheItem<TKey, TValue>> _list = new LinkedList<CacheItem<TKey, TValue>> ();
		private readonly long _itemCapacity;
		#endregion


		public AssociativeCache(long itemCapacity, IEvictionPolicy<TKey, TValue> evictionPolicy = null)
		{
			_evictionPolicy = evictionPolicy;
			if (_evictionPolicy == null)
			{
				_evictionPolicy = new LruEviction<TKey, TValue>();
			}

			_itemCapacity = itemCapacity;
			_items = new Dictionary<TKey, CacheItem<TKey, TValue>>();
		}


		#region Public Methods

		public void Add(TKey key, TValue value)
		{
			CacheItem<TKey, TValue> item = new CacheItem<TKey, TValue>(key, value);
			Add(item);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Add(CacheItem<TKey, TValue> item)
		{
			// Evict / Remove using policy if capacity has reached
			if(_items.Count >= _itemCapacity)
			{
				TKey keyRemoved = _evictionPolicy.RemoveItem(ref _list);						

				// Remove from cache
				_items.Remove(keyRemoved);
			}

			// Add using Policy
			_evictionPolicy.AddItem(ref _list, item);

			// Add items
			_items.Add(item.Key, item);
		}

		public int Count
		{
			get { return _items.Count; }
		}

		public void Flush()
		{
			_items.Clear();
			_list.Clear();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public TValue Get(TKey key)
		{
			CacheItem<TKey, TValue> item = null;
			try
			{
				item = _items[key];

			}
			catch (KeyNotFoundException exception)
			{
				return null;
			}

			return item.Value;

		}
#endregion


		#region Private Methods
		private void ReplaceItem()
		{
			

		}

		/// <summary>
		/// String override to assist in debugging and Visual Studio visualizations
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			/// TODO - Put a #Debug compilation block 
		
			string output = string.Format("Item Count: {0} List Count: {1}", _items.Count, _list.Count);

			foreach(KeyValuePair<TKey, CacheItem < TKey, TValue >> item in _items)
			{
				output += string.Format(@"{0}{2}", item.ToString(), Environment.NewLine);				
			}

			return output;
		}
		#endregion

	}
}
