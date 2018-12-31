using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Presentation.ActionFilter;
using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nagarro.Gitter.Presentation.Controllers
{
    public class AnalyticsController : ApiController
    {
        
        [UserAuthFilter]
        [HttpGet]
        public HttpResponseMessage GetAnalytics()
        {
            IAnalyticsManager _analyticsManger = BusinessFactory.GetAnalyticsMangerObj();
            try
            {
                AnalyticsDTO analyticsDTO = _analyticsManger.GetAnalytics();
                DataDTO<AnalyticsDTO> Data = new DataDTO<AnalyticsDTO>();
                Data.data = analyticsDTO;
                return Request.CreateResponse(HttpStatusCode.Created, Data);
            }
            catch (Exception)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "User not found";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
    }
}
