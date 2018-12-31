using Nagarro.Gitter.Entites.Concerte;
using Nagarro.Gitter.Entites.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository
{

    public class EntitesFactory
    {
        public static GitterContext GetEntitesObj()
        {
            return new GitterContext();
        }
    }
}
