using Nagarro.Gitter.Repository.Concrete;
using Nagarro.Gitter.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business
{
   public class RepositoryFactory
    {
        public static IUser GetUserContextObj()
        {
            return new UserContext();
        }
        public static ITweetContext GetTweetContextObj()
        {
            return new TweetContext();
        }
        public static IFollowContext GetFollowContextObj()
        {
            return new FollowContext();
        }
        
    }
}
