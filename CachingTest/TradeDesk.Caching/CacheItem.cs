using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Caching
{
	/// <summary>
	/// Single Item that represent a Cache Item. This can be used for any of the caching classes in the library
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class CacheItem<TKey, TValue>
	{	
		public CacheItem(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public TValue Value { get; set; }

		public TKey Key { get; set; }
		
	
		public DateTime ExpirationTime { get; set; }


		public override string ToString()
		{
			return string.Format("{Key: {0} Value {1}", this.Key, this.Value);
		}


	}
}
