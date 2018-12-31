using Nagarro.Gitter.Entites.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Entity
{
    public class TweetHashTagMap : IEntity
    {

        [Key]
        public Guid ID { get; set; }
        public Guid HashTagID { get;set;}
        public Guid TweetID { get;set;}

        public virtual HashTag HashTags { get; set; }
        public virtual Tweet Tweets { get; set; }

    }
}
