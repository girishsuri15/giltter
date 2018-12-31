using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business.Contract
{
  public  interface IAnalyticsManager
    {
        AnalyticsDTO GetAnalytics();
       
    }
}
