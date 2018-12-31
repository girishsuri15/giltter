using Nagarro.Gitter.Entites.Contract;
using Nagarro.Gitter.Entites.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Concerte
{
   
  public class GitterContext : DbContext ,IGitterContext
    {
            public GitterContext() : base("GitterDataBaseConn")
            {

            }
            public IDbSet<User> Users { get; set; }
            public IDbSet<Tweet> Tweets { get; set; }
            public IDbSet<HashTag> HashTags { get; set; }
            public IDbSet<TweetHashTagMap> TweetHashTagMaps { get; set; }
            public IDbSet<UserTweetVote> UserTweetVotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Follower).WithMany(x => x.Following)
                .Map(x => x.ToTable("UserFollowerMaps")
                    .MapLeftKey("Follower")
                    .MapRightKey("Following"));
        }
    }


   

}

