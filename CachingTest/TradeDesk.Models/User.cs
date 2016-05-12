using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Models
{
	public class User : ModelBase
	{

		public string Name { get; set; }
		public int Age { get; set; }

		public IList<BlogPost> BlogPosts { get; set; }

		public User()
		{
			Id = Guid.NewGuid();
			BlogPosts = new List<BlogPost>();
		}
    }
}
