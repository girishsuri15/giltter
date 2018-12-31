using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business.Concrete
{
   public class AnalyticsManager: IAnalyticsManager
    {
        ITweetContext _tweet;
        public AnalyticsManager()
        {
           _tweet = RepositoryFactory.GetTweetContextObj();
        }
      public   AnalyticsDTO GetAnalytics()
        {
            AnalyticsDTO analyticsDTO = new AnalyticsDTO();

            analyticsDTO.HashTag = _tweet.GetHashTag();

            analyticsDTO.TweetCount = _tweet.GetTweetToday();

            analyticsDTO.UserName = _tweet.GetMostActivePerson();

            analyticsDTO.TweetName = _tweet.GetMostLikeTweet();
            return analyticsDTO;
        }

    }
       
}
