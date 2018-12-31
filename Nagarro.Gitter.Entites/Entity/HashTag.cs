using Nagarro.Gitter.Entites.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Entity
{
    public class HashTag : IEntity
    {
        public HashTag()
        {
            this.TweetHashTagMaps = new HashSet<TweetHashTagMap>();
        }
        public Guid ID { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string HashTagDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<TweetHashTagMap> TweetHashTagMaps { get; set; }

    }
}
