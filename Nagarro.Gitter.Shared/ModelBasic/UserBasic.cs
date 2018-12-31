using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nagarro.Gitter.Shared.ModelBasic
{
    public class UserBasic
    {
        public Guid ID { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public byte[] Image { get; set; }

        public string Country { get; set; }

        public string ContactNumber { get; set; }
    }
}