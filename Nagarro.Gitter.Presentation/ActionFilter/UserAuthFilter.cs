using Nagarro.Gitter.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Nagarro.Gitter.Presentation.ActionFilter
{
    public class UserAuthFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // Gets header parameters  
                string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                try
                {
                    Guid Id = new Guid(authenticationString);
                    if (!((BusinessFactory.GetUserMangerObj()).ValidateUser(Id)) )
                    {
                        // returns unauthorized error  
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                    actionContext.Request.Properties.Add("mykey", Id);
                }
                catch (Exception)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
               
            }
            //Session["UserID"] = Id;
            
            base.OnAuthorization(actionContext);
        }
    }
}