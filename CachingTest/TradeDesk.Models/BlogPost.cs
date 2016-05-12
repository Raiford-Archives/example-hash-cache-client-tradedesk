using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Models
{
    public class BlogPost : ModelBase
	{
		public string Subject { get; set; }

		public string Body { get; set; }

		public DateTime PostDate { get; set; }


    }
}
