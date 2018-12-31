using Nagarro.Gitter.Business.Concrete;
using Nagarro.Gitter.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nagarro.Gitter.Presentation
{
    public class BusinessFactory
    {

        public static IUserManager GetUserMangerObj()
            {
                 return new UserManager();
            }
        public static ITweetManager GetTweetMangerObj()
        {
            return new TweetManager();
        }
        public static IFollowManager GetFollowMangerObj()
        {
            return new FollowManager();
        }

        public static IAnalyticsManager GetAnalyticsMangerObj()
        {
            return new AnalyticsManager();
        }
    }
}