using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business.Contract
{
   public interface ITweetManager
    {
       TweetDTO CreateTweet(TweetModel tweetModel,Guid ID);
        TweetsDTO GetTweetByUser(Guid ID);
        bool AddVote(Guid UserID, Guid TweetID, string Reaction);
        void DeleteTweet(Guid ID, Guid TweetID);
       TweetDTO EditTweet(TweetModel tweetModel, Guid ID, Guid TweetID);
        TweetsDTO SearchHashTags(string search);

    }
}
