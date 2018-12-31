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
    [UserAuthFilter]
    [RoutePrefix("api/Follow")]
    public class FollowController : ApiController
    {
        [Route("Follower")]
        [HttpGet]
        public HttpResponseMessage GetUserFollowerCount()
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            IFollowManager _followManger = BusinessFactory.GetFollowMangerObj();
            DataDTO<int> Data = new DataDTO<int>();
            Data.data = _followManger.CountFollower(ID);
            return Request.CreateResponse(HttpStatusCode.OK, Data);
        }

        [Route("Following")]
        [HttpGet]
        public HttpResponseMessage GetUserFollowingCount()
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            IFollowManager _followManger = BusinessFactory.GetFollowMangerObj();
            DataDTO<int> Data = new DataDTO<int>();
            Data.data = _followManger.CountFollowing(ID);
            return Request.CreateResponse(HttpStatusCode.OK, Data);
        }

        [Route("User/Follower")]
        [HttpGet]
        public HttpResponseMessage GetUserAllFollowers()
        {
            try
            {
                object Token;
                Request.Properties.TryGetValue("mykey", out Token);
                Guid ID = new Guid(Token.ToString());
                IFollowManager _followManger = BusinessFactory.GetFollowMangerObj();
                DataDTO<UsersDTO> Data = new DataDTO<UsersDTO>();
                Data.data = _followManger.UserFollower(ID);
                return Request.CreateResponse(HttpStatusCode.OK, Data);
            }
            catch (Exception)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "internal error";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }

        [Route("User/Following")]
        [HttpGet]
        public HttpResponseMessage GetUserAllFollowings()
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            try
            {
                IFollowManager _followManger = BusinessFactory.GetFollowMangerObj();
                DataDTO<UsersDTO> Data = new DataDTO<UsersDTO>();
                Data.data = _followManger.UserFollowing(ID);
                return Request.CreateResponse(HttpStatusCode.OK, Data);
            }
            catch (Exception)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "internal error";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }

        [Route("{Following}")]
        [HttpPost]
        public HttpResponseMessage FollowUser([FromUri] Guid Following)
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            //this._followBDC.FollowUser(followDTO.ID, followDTO.FollowingId);
            IFollowManager _followManger = BusinessFactory.GetFollowMangerObj();
            bool t=_followManger.AddFollowing(ID, Following);
            if (t)
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "You cannnot follow";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
        [Route("{Following}")]
        [HttpDelete]
        public HttpResponseMessage UnfollowUser([FromUri]  Guid Following)
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            IFollowManager _followManger = BusinessFactory.GetFollowMangerObj();
            _followManger.RemoveFollowing(ID,Following);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
