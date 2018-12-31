using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Shared.DTO
{
   public class UserTweetVotesDTO
    {
        public Guid ID { get; set; }
        public int Vote { get; set; }
        public Guid TweetID { get; set; }
        public virtual UserDTO Users { get; set; }
    }
}
