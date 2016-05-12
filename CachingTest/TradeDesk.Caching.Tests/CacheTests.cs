using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using TradeDesk.Models;
using System.Collections.Generic;
using TradeDesk.Caching;
using TradeDesk.Caching.Plugins;

namespace TradeDesk.Caching.Tests
{
	[TestClass]
	public class CacheTests
	{
	
		[TestMethod]
		public void AssociativeCache_BasicUsage()
		{
			string myKey = "12345";
			int itemCount = 100;

			//////////////////////////////////////////
			// IMPORTANT: Choose your EvictionPolicy
			//////////////////////////////////////////
			//IEvictionPolicy<string, string> evictionPolicy = new LruEviction<string, string>();
			IEvictionPolicy<string, string> evictionPolicy = new MruEviction<string, string>();



			IAssociativeCache<string, string> cache = new AssociativeCache<string, string>(itemCount, evictionPolicy);



			/////////////////////////////////////////
			// TEST 1). Get null value
			/////////////////////////////////////////
			string cachedValue = cache.Get(myKey);
			Assert.IsTrue(string.IsNullOrWhiteSpace(cachedValue));


			//////////////////////////////////////////////////////////////////////
			// TEST 2). Add Items and ensure that items are removed when count hit
			//////////////////////////////////////////////////////////////////////
			for ( int i = 1; i < itemCount+10; i++)
			{
				cache.Add("Key" + i, "Value" + i);

				if(i >= itemCount)
				{
					Assert.IsTrue(cache.Count == itemCount);
				}
				else
				{
					Assert.IsTrue(cache.Count == i);
				}
			}


			//////////////////////////////////////////////////////////////////////
			// TEST 3). Add a few items and Check counts
			//////////////////////////////////////////////////////////////////////
			cache = new AssociativeCache<string, string>(2);

			cache.Add("1", "First");
			Assert.IsTrue(cache.Count == 1);

			cache.Add("2", "Second");
			Assert.IsTrue(cache.Count == 2);

			cache.Add("3", "Third");
			Assert.IsTrue(cache.Count == 2); // NOTE: this remains the same

			cache.Add("4", "Fourth");
			Assert.IsTrue(cache.Count == 2); // NOTE: this remains the same



			//////////////////////////////////////////////////////////////////////
			// TEST 4). Add and Get from cache
			/////////////////////////////////////////////////////////////////////
			cache.Flush();

			cache.Add("1", "First");
			Assert.IsTrue(cache.Get("1") == "First");

			cache.Add("2", "Second");
			Assert.IsTrue(cache.Get("2") == "Second");

			cache.Add("3", "Third");
			Assert.IsTrue(cache.Get("3") == "Third");

			cache.Add("4", "Fourth");
			Assert.IsTrue(cache.Get("4") == "Fourth");

			// And finally the first 2 should now be null
			Assert.IsTrue(cache.Get("1") == null);

			Assert.IsTrue(cache.Get("2") == null);

			// Should still be count 2
			Assert.IsTrue(cache.Count == 2);
		}
	


		[TestMethod]
		public void SetAssociativeCache_BasicUsage()
		{
			string myKey = "12345";
			int itemCount = 100;
			int slotSize = 100;
			SetAssociativeCache<string, string> cache = new SetAssociativeCache<string, string>(slotSize, itemCount);



			/////////////////////////////////////////
			// TEST 1). Get null value
			/////////////////////////////////////////
			string cachedValue = cache.Get(myKey);
			Assert.IsTrue(string.IsNullOrWhiteSpace(cachedValue));


			//////////////////////////////////////////////////////////////////////
			// TEST 2). Add Items and ensure that items are removed when count hit
			//////////////////////////////////////////////////////////////////////
			for (int i = 1; i < itemCount + 10; i++)
			{
				string k = "Key " + i;
				string v = "Value " + i;

				cache.Add(k, v);

				if (i >= itemCount)
				{
					//Assert.IsTrue(cache.GetCount(k) == itemCount);
				}
				
				cachedValue = cache.Get(k);
				Assert.IsTrue(cachedValue == v);

			}


			//////////////////////////////////////////////////////////////////////
			// TEST 3). Add a few items and Check counts
			//////////////////////////////////////////////////////////////////////
			cache = new SetAssociativeCache<string, string>(1, 2);

			cache.Add("1", "First");
			Assert.IsTrue(cache.GetCount("1") == 1);

			cache.Add("2", "Second");
			Assert.IsTrue(cache.GetCount("2") == 2);

			cache.Add("3", "Third");
			Assert.IsTrue(cache.GetCount("3") == 2); // NOTE: this remains the same

			cache.Add("4", "Fourth");
			Assert.IsTrue(cache.GetCount("4") == 2); // NOTE: this remains the same



			//////////////////////////////////////////////////////////////////////
			// TEST 4). Add and Get from cache
			/////////////////////////////////////////////////////////////////////
			cache.Flush();

			cache.Add("1", "First");
			Assert.IsTrue(cache.Get("1") == "First");

			cache.Add("2", "Second");
			Assert.IsTrue(cache.Get("2") == "Second");

			cache.Add("3", "Third");
			Assert.IsTrue(cache.Get("3") == "Third");

			cache.Add("4", "Fourth");
			Assert.IsTrue(cache.Get("4") == "Fourth");

			// And finally the first 2 should now be null
			Assert.IsTrue(cache.Get("1") == null);

			Assert.IsTrue(cache.Get("2") == null);

			// Should still be count 2
			Assert.IsTrue(cache.GetCount("4") == 2);



		}

	}
}
