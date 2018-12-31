using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository.Contract
{
    public interface ITweetContext
    {
        Guid CreateTweet(string desc, Guid ID);
        IEnumerable<Guid> CreateHashTag(string[] HashTag);
        bool CreateTweetHash(Guid TweetId, IEnumerable<Guid> HashTagIDs);
        TweetDTO GetTweet(Guid TweetId);
        TweetsDTO GetTweetByUser(Guid UserID);
        bool AddVote(Guid UserID, Guid TweetID,int VoteType);
        bool DeleteTweet(Guid UserID, Guid TweetID);
        TweetDTO EditTweet(TweetModel Tweet, Guid ID, Guid TweetID);
        TweetsDTO SearchHashTags(string search);
        void DeleteHashTag(Guid TweetID);

     string   GetHashTag();

       int GetTweetToday();

       string GetMostActivePerson();

       string GetMostLikeTweet();

    }
}
