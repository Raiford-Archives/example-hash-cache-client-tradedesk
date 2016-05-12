using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Caching.Plugins
{
	public interface IHashFunction<TKey>
	{
		int GenerateHash(int slotCount, TKey key);
	}

}
