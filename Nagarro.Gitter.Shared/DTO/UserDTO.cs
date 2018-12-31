using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Shared.DTO
{
   public class UserDTO
    {
        
        public Guid ID { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public byte[] Image { get; set; }

        public string Country { get; set; }

        public string ContactNumber { get; set; }
    }
}
