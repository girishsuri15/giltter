using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Business.Exceptions;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business.Concrete
{
   public class TweetManager:ITweetManager
    {
        private readonly ITweetContext _tweet;
        public TweetManager()
        {
            _tweet = RepositoryFactory.GetTweetContextObj();
        }
       public TweetDTO CreateTweet(TweetModel tweetModel,Guid ID)
        {
            Guid  TweetId = _tweet.CreateTweet(tweetModel.Tweet_Descrption, ID);
            IEnumerable<Guid> HashTagIDs= _tweet.CreateHashTag(tweetModel.HashTag);
            bool IsInserted = _tweet.CreateTweetHash(TweetId, HashTagIDs);
            if (IsInserted)
            {
                TweetDTO tweetDTO = _tweet.GetTweet(TweetId);
                return tweetDTO;
            }
            
            else
            {
                throw new Exception();
            }
        }
        public TweetsDTO GetTweetByUser(Guid ID)
        {
           return _tweet.GetTweetByUser(ID);
        }
     public   bool AddVote(Guid UserID, Guid TweetID,string Reaction)
        {
            if (Reaction.ToLower() == "like")
            {
                return _tweet.AddVote(UserID, TweetID, 1);
            }
            else if (Reaction.ToLower() == "dislike")
            {
                return _tweet.AddVote(UserID, TweetID, 0);
            }
            else
                return false;
            
        }
   public  void DeleteTweet(Guid ID, Guid TweetID)
        {
            bool isDeleted =_tweet.DeleteTweet(ID, TweetID);
            if (!isDeleted)
            {
                throw new Exception();
            }
        }
     public TweetDTO EditTweet(TweetModel Tweet, Guid ID, Guid TweetID)
        {
            
          try
            {
                _tweet.DeleteHashTag(TweetID);
                return _tweet.EditTweet(Tweet, ID, TweetID);
            }
            catch(Exception ex)
            {
                throw new TweetBelongsToOtherException(); 
            }
        }
        public TweetsDTO SearchHashTags(string search)
        {
            return _tweet.SearchHashTags(search);
        }

    }
}
