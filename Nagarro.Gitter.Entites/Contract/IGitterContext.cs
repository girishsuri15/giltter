using Nagarro.Gitter.Entites.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Contract
{
    public interface IGitterContext 
    {
         IDbSet<User> Users { get; set; }
         IDbSet<Tweet> Tweets { get; set; }
         IDbSet<HashTag> HashTags { get; set; }
         IDbSet<TweetHashTagMap> TweetHashTagMaps { get; set; }
         IDbSet<UserTweetVote> UserTweetVotes { get; set; }
    }
}
