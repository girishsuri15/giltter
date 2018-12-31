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
    public class User : IEntity
    {
        public User()
        {
            this.Tweets = new HashSet<Tweet>();
        }
        [Key]
        public Guid ID { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public string Hash { get; set; }

        public string UserName { get; set; }

        public byte[] Image { get; set; }

        public string Country { get; set; }

        public string ContactNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<User> Follower { get; set; }
        public virtual ICollection<User> Following { get; set; }
       


    }
}
