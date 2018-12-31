using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Entites.Contract
{
    public interface IEntity
    {
        Guid ID { get; set; }
    }
}
