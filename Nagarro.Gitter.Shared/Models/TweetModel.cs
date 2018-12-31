using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Shared.Models
{
   public  class TweetModel
    {
        [Required]
        [MaxLength(240)]
        public string Tweet_Descrption { get; set; }
        public string[] HashTag { get; set; }
    }
}
