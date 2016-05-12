using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Caching.Plugins
{
	public class DefaultHashFunction<TKey> : IHashFunction<TKey>
	{
		public int GenerateHash(int slotCount, TKey key)
		{
			int hash = 0;
			int sum = 0;

			string stringKey = key.ToString();

			for (int i = 0; i < stringKey.Length; i++)
			{
				sum += (int)stringKey[i];
			}

			// Hash Here
			hash = HashInt(sum, slotCount);

			// Return Calculated Cache
			return hash;

		}

		int HashInt(int key, int slotCount)
		{
			return key % slotCount;
		}
	}
}
