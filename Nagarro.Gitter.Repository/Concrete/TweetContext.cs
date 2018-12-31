using AutoMapper;
using Nagarro.Gitter.Entites.Concerte;
using Nagarro.Gitter.Entites.Entity;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Repository.Exceptions;
using Nagarro.Gitter.Shared;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository.Concrete
{
   public class TweetContext: ITweetContext
    {
        private readonly GitterContext _dbContext;
        private readonly IMapper _Mapper;
        public TweetContext()
        {
            _dbContext = EntitesFactory.GetEntitesObj();
             _Mapper = MapperFactory.GetMapperObj();
        }
        public Guid CreateTweet(string desc, Guid ID)
        {
            Tweet newtweet = new Tweet();
            User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
            newtweet.Tweet_Descrption = desc;
            newtweet.CreationDate = System.DateTime.Now;
            newtweet.ModificationDate = System.DateTime.Now;
            newtweet.ID=Guid.NewGuid();
           newtweet.User = user;
            newtweet.UserID = user.ID;
            _dbContext.Tweets.Add(newtweet);
            _dbContext.SaveChanges();
            return newtweet.ID;
        }
        public IEnumerable<Guid> CreateHashTag(string[] HashTag)
        {
            HashTag newHash;
            List<Guid> HashTagID = new List<Guid>();
            foreach(string hash in HashTag)
            {
                 
                HashTag existhashTag = _dbContext.HashTags.Where(u => u.HashTagDescription == hash).FirstOrDefault();
                if (existhashTag == null)
                {
                    newHash = new HashTag();
                    newHash.ID = Guid.NewGuid();
                    newHash.HashTagDescription = hash;
                    newHash.CreationDate = System.DateTime.Now;
                    _dbContext.HashTags.Add(newHash);
                    HashTagID.Add(newHash.ID);
                }
                else
                {
                    HashTagID.Add(existhashTag.ID);
                }
                
            }
            try {
                _dbContext.SaveChanges();
                return HashTagID;
            }
            catch(Exception ex)
            {
                throw new HashTagNotAdded();
            }
            
        }
       public bool CreateTweetHash(Guid TweetId, IEnumerable<Guid> HashTagIDs)
        {
            TweetHashTagMap tweetHashTagMap;
            Tweet tweet = _dbContext.Tweets.Where(u => u.ID == TweetId).FirstOrDefault();
            foreach (Guid ID in HashTagIDs)
            {
                HashTag hashTag = _dbContext.HashTags.Where(u => u.ID == ID).FirstOrDefault();
                tweetHashTagMap = new TweetHashTagMap();
                tweetHashTagMap.ID = Guid.NewGuid();
                tweetHashTagMap.Tweets = tweet;
                tweetHashTagMap.HashTags = hashTag;
                tweetHashTagMap.HashTagID = hashTag.ID;
                tweetHashTagMap.TweetID = tweet.ID;
                _dbContext.TweetHashTagMaps.Add(tweetHashTagMap);
            }
            _dbContext.SaveChanges();
            return true;
        }
       public TweetDTO GetTweet(Guid TweetId)
        {
            Tweet tweet = _dbContext.Tweets.Where(t => t.ID == TweetId).FirstOrDefault();
            
            TweetDTO tweetDTO = _Mapper.Map<Tweet, TweetDTO>(tweet);
            return tweetDTO;
        }
        public TweetsDTO GetTweetByUser(Guid UserID)
        {
            List<Tweet> tweets = new List<Tweet>();
            List<Tweet> UserTweets = new List<Tweet>();
            List<Tweet> FollowingsTweets;
            User user = _dbContext.Users.Where(t => t.ID == UserID).FirstOrDefault();
            UserTweets = _dbContext.Tweets.Where(t => t.UserID == UserID).ToList();
            foreach (Tweet usertweet in UserTweets)
            {
                    tweets.Add(usertweet);
            }
            foreach (User Following in user.Follower)
            {
                FollowingsTweets = new List<Tweet>();
                FollowingsTweets = _dbContext.Tweets.Where(t => t.UserID == Following.ID).ToList();
                foreach(Tweet singletweet in FollowingsTweets)
                {
                    tweets.Add(singletweet);
                }
            }

            TweetsDTO tweetsDTO = new TweetsDTO();
            tweetsDTO.Tweets =_Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDTO>>(tweets);

            tweetsDTO.Tweets= tweetsDTO.Tweets.OrderByDescending(t => t.CreationDate);
            return tweetsDTO;
        }
        public bool AddVote(Guid UserID, Guid TweetID,int VoteType)
        {
            User user = _dbContext.Users.Where(t => t.ID == UserID).FirstOrDefault();
            Tweet tweet = _dbContext.Tweets.Where(t => t.ID == TweetID).FirstOrDefault();
            UserTweetVote voteTweet = _dbContext.UserTweetVotes.Where(t => t.TweetID == TweetID && t.Users.ID==user.ID).FirstOrDefault();


            if(voteTweet == null)
            {
                voteTweet = new UserTweetVote();
                voteTweet.ID= Guid.NewGuid();
                voteTweet.TweetID = tweet.ID;
                voteTweet.Vote = VoteType;
                voteTweet.Users = user;
                _dbContext.UserTweetVotes.Add(voteTweet);
            }
            else
            {
                voteTweet.Vote = VoteType;
                
            }
            _dbContext.SaveChanges();
            return true;
        }
      public  bool DeleteTweet(Guid UserID, Guid TweetID)
        {
            User user = _dbContext.Users.Where(t => t.ID == UserID).FirstOrDefault();
            Tweet tweet = _dbContext.Tweets.Where(t => t.ID == TweetID).FirstOrDefault();
            if (tweet.UserID == user.ID)
            {
                IEnumerable<TweetHashTagMap> tweetHashs = _dbContext.TweetHashTagMaps.Where(t => t.TweetID == TweetID).ToList();
                foreach (TweetHashTagMap tweetHash in tweetHashs)
                {
                    _dbContext.TweetHashTagMaps.Remove(tweetHash);
                }

                _dbContext.Tweets.Remove(tweet);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public TweetDTO EditTweet(TweetModel Tweet, Guid ID,Guid TweetID)
        {
            
            User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
            Tweet tweet = _dbContext.Tweets.Where(t => t.ID == TweetID ).FirstOrDefault();
            if (user.ID == tweet.UserID)
            {
                
                tweet.Tweet_Descrption = Tweet.Tweet_Descrption;
                tweet.ModificationDate = System.DateTime.Now;
                _dbContext.SaveChanges();
                TweetDTO tweetDTO = _Mapper.Map<Tweet, TweetDTO>(tweet);
                return tweetDTO;
            }
            else
            {
                throw new Exception();
            }

            
        }
       public TweetsDTO SearchHashTags(string search)
        {
           // IEnumerable<HashTag> hashtags = _dbContext.HashTags.Where(t => t.HashTagDescription.Contains(search)).ToList();
           
               IEnumerable<Tweet> Tweets = _dbContext.TweetHashTagMaps.Where(t => t.HashTags.HashTagDescription.Contains(search)).Select(t=>t.Tweets).ToList();

              //
              TweetsDTO tweetsDTO = new TweetsDTO();
            tweetsDTO.Tweets = _Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDTO>>(Tweets);
            return tweetsDTO;
        }
        public string GetHashTag()
        {
            Guid HashTagID = _dbContext.TweetHashTagMaps.GroupBy(t => t.HashTagID).OrderByDescending(g => g.Count()).FirstOrDefault().Select(d=>d.HashTagID).FirstOrDefault();
            string hashtag =_dbContext.HashTags.Where(t => t.ID==HashTagID).Select(d =>d.HashTagDescription).FirstOrDefault();
            return hashtag;
        }

        public int GetTweetToday()
        {
            DateTime date= System.DateTime.Now.Date;
            int TweetCount =_dbContext.Tweets.Where(q => q.CreationDate>= date).Select(d => d.CreationDate).Count();
            return TweetCount;
        }

        public string GetMostActivePerson()
        {
            Guid UserID = _dbContext.Tweets.GroupBy(t => t.UserID).OrderByDescending(gp => gp.Count()).FirstOrDefault().Select(d => d.UserID).FirstOrDefault();
            string UserName = _dbContext.Users.Where(t => t.ID == UserID).Select(d => d.UserName).FirstOrDefault();
            return UserName;
        }

        public string GetMostLikeTweet()
        {
            Guid TweetID= _dbContext.UserTweetVotes.GroupBy(t => t.TweetID).OrderByDescending(gp => gp.Count()).FirstOrDefault().Select(d => d.TweetID).FirstOrDefault();
            string Tweet = _dbContext.Tweets.Where(t => t.ID == TweetID).Select(d => d.Tweet_Descrption).FirstOrDefault();
            return Tweet;
        }
        public void DeleteHashTag(Guid TweetID)
        {
            IEnumerable<TweetHashTagMap> tweetHashs = _dbContext.TweetHashTagMaps.Where(t => t.TweetID == TweetID).ToList();
            foreach (TweetHashTagMap tweetHash in tweetHashs)
            {
                _dbContext.TweetHashTagMaps.Remove(tweetHash);
            }

            _dbContext.SaveChanges();
        }
    }
}
