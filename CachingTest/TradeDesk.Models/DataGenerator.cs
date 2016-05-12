using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeDesk.Models
{
	public class DataGenerator
	{
		public static IList<User> CreateUsers(int count, int postCount=0)
		{
			IList<User> users = new List<User>(count);

			for(int i = 0; i < count; i++)
			{
				User u = new User()
				{
					Id = Guid.NewGuid(),
					Age = DataGenerator.GenerateAge(),
					Name = "Test Name " + i.ToString()
				};

				for(int j=0; j < postCount; j++)
				{
					BlogPost post = new BlogPost()
					{
						Id = Guid.NewGuid(),
						PostDate = DateTime.Now,
						Subject = "Post " + j.ToString(),
						Body = "This is the body of the post " + j.ToString()
					};

					u.BlogPosts.Add(post);
				}							

				users.Add(u);
			}
			return users;
			
		}

		private static int GenerateAge()
		{
			Random random = new Random();
			return random.Next(20, 70);
		}

		
	}
}
