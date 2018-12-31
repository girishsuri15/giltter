using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Shared.DTO
{
   public class TweetDTO
    {
        
        public string Tweet_Descrption { get; set; }
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual UserDTO user { get; set; }
       public virtual  IEnumerable<UserTweetVotesDTO> UserTweetVotes{ get; set; }

    }
}
