using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeDesk.Caching;
using TradeDesk.Caching.Plugins;

namespace TradeDesk.Caching
{
	public class SetAssociativeCache<TKey, TValue> where TValue : class
	{
		private Dictionary<int, IAssociativeCache<TKey, TValue>> _hashTable;
		private IHashFunction<TKey> _hashFunction = new DefaultHashFunction<TKey>();
		private IEvictionPolicy<TKey, TValue> _evictionPolicy = new LruEviction<TKey, TValue>();
		private int _slotSize;
		private int _itemSize;


		public SetAssociativeCache(int slotSize, int itemSize, IHashFunction<TKey> hashFunction = null, IEvictionPolicy<TKey, TValue> evictionPolicy = null)
		{
			// Assign the HashFunction implementation
			_hashFunction = hashFunction;
			if (hashFunction == null)
			{
				_hashFunction = new DefaultHashFunction<TKey>();
			}

			// Assign the EvictionPolicy implementation
			_evictionPolicy = evictionPolicy;
			if (evictionPolicy == null)
			{
				_evictionPolicy = new LruEviction<TKey, TValue>();
			}


			_hashTable = new Dictionary<int, IAssociativeCache<TKey, TValue>>(slotSize);
			_slotSize = slotSize;
			_itemSize = itemSize;


			// Initialize Slots
			for (int i = 0; i < _slotSize; i++)
			{
				_hashTable.Add(i, new AssociativeCache<TKey, TValue>(_itemSize, _evictionPolicy));
			}

		}

		public void Add(TKey key, TValue value)
		{
			CacheItem<TKey, TValue> item = new CacheItem<TKey, TValue>(key, value);
			Add(item);

		}

		public void Add(CacheItem<TKey, TValue> item)
		{
			// Create the Hash
			int slotHash = _hashFunction.GenerateHash(_slotSize, item.Key);

			// Add to Associated Cache
			_hashTable[slotHash].Add(item);


		}

		public TValue Get(TKey key)
		{
			// Create the Hash
			int slotHash = _hashFunction.GenerateHash(_slotSize, key);

			// Get from Associated Cache
			return _hashTable[slotHash].Get(key);
		}

		public int GetCount(TKey key)
		{
			int slotHash = _hashFunction.GenerateHash(_slotSize, key);

			return _hashTable[slotHash].Count;
		}

		public void Flush()
		{
			// Iterate the Hash Table and Flush the Associated Cache for each
			foreach (var item in _hashTable)
			{
				item.Value.Flush();
			}
		}

	}

}
