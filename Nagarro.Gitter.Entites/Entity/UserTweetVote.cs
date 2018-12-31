using Nagarro.Gitter.Entites.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Entity
{
    public class UserTweetVote : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public int Vote { get; set; }
        public Guid TweetID { get; set; }
        
        public virtual User Users { get; set; }
        public virtual Tweet Tweets { get; set; }

    }
}
