using Nagarro.Gitter.Entites.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Entity
{
    public class Tweet : IEntity
    {
        public Tweet()
        {
            this.TweetHashTagMaps = new HashSet<TweetHashTagMap>();
            this.UserTweetVotes = new HashSet<UserTweetVote>();
            
        }
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(240)]
        public string Tweet_Descrption { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
        public Guid UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<TweetHashTagMap> TweetHashTagMaps { get; set; }
        public virtual ICollection<UserTweetVote> UserTweetVotes { get; set; }

    }
}
