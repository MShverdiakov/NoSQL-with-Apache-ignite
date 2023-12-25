using System;
using System.Collections;
using System.Collections.Generic;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;
using Apache.Ignite.Core.Binary;


namespace IgniteTest
{
	class subject
	{
		public string a;
		public int b;
		public object c;
		public subject()
		{
			a = "0";
			b = 0;
			c = "";
		}
		public subject(string a, int b, int c)
		{
			this.a = a;
			this.b = b;
			this.c = c;
		}

	}
	class Program
	{
		static void Main(string[] args)
		{
			
			var cfg = new IgniteClientConfiguration ("127.0.0.1:10800");

			using (var client = Ignition.StartClient(cfg))
			{
				var cache = client.GetOrCreateCache<int, string>("cache");
				var cache1 = client.GetOrCreateCache<int, int>("cache1");
				var cache2 = client.GetOrCreateCache<int, object>("cache2");
				cache.Clear();
				cache.Put(1, "Вариант №9, Apach-Ignate");
				cache1.Put(2, 1234);

				
				byte[] B = { 1, 2, 3, 4 };
				String value3 = "";
				foreach (byte item in B)
					value3 += item + " ";
				cache.Put(3, value3);

				List<String> L = new List<String>();
				L.Add("ldofg");
				L.Add("Word");
				L.Add("kjdsnf");
				L.Add("kjsdf,");
				L.Add("klfjkdfnjlndf");
				String value4 = "";
				foreach (String item in L)
					value4 += item + " ";
				cache.Put(4, value4);


				ISet<Object> set = new HashSet<Object>();
				set.Add("value");
				set.Add(657.91);
				set.Add(123);
				String value5 = "";
				foreach (Object item in set)
					value5 += item + " ";
				cache.Put(5, value5);

				Hashtable h = new Hashtable();
				h.Add("string", "hfdkhjbgkh");
				h.Add("float", 1823.085);
				h["int"] = 1;
				String value6 = "";
				foreach (DictionaryEntry item in h)
					value6 += "     key: " + item.Key + ", value = " + item.Value + "\n";
				cache.Put(6, value6);

				DateTime dt = DateTime.Now;
				subject mc = new subject() { a = "word" , b = 112, c = dt};
				cache2.Put(7, mc);

				var binary = client.GetBinary();
				var val = binary.GetBuilder("Person")
					.SetField("id", 1)
					.SetField("name", "Mike")
					.Build();
				var cache5 = client.GetOrCreateCache<int, object>("persons").WithKeepBinary<int, IBinaryObject>();
				cache5.Put(9, val);



				
				Console.WriteLine("\nKey 1 \"text\": value = " + cache.Get(1));
				Console.WriteLine("Key 2 \"namb\": value = " + cache1.Get(2));
				Console.WriteLine("Key 3 \"bkey\": value = " + cache.Get(3));
				Console.WriteLine("Key 4 \"List\": value = " + cache.Get(4));
				Console.WriteLine("Key 5 \"HashSet\": value = " + cache.Get(5));
				Console.WriteLine("Key 6 \"Hashtable\": value = \n" + cache.Get(6));
				subject kr = (subject)cache2.Get(7);
				Console.WriteLine("Key 7 \"My_Class\": Sting={0}, Namb={1}, Data={2} ",kr.a, kr.b, kr.c);
				
				cache.Clear();
				cache1.Clear();
				cache2.Clear();
				Console.ReadKey();

			}
		}
	}
}