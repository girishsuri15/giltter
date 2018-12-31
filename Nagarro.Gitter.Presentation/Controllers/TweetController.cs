using AutoMapper;
using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Presentation.ActionFilter;
using Nagarro.Gitter.Shared;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nagarro.Gitter.Presentation.Controllers
{
    [RoutePrefix("api/Tweet")]
    [UserAuthFilter]
    public class TweetController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostTweet([FromBody] TweetModel tweetModel)
        {
            object Token;
            Request.Properties.TryGetValue("mykey",out Token);
            Guid ID = new Guid(Token.ToString());

            if (ModelState.IsValid)
            {
                ITweetManager _TweetManger = BusinessFactory.GetTweetMangerObj();
                IMapper Mapper = MapperFactory.GetMapperObj();

                try
                {
                    TweetDTO TweetDTO = _TweetManger.CreateTweet(tweetModel, ID);
                    DataDTO<TweetDTO> Data = new DataDTO<TweetDTO>();
                    Data.data = TweetDTO;


                    return Request.CreateResponse(HttpStatusCode.Created, Data);
                }
                catch (Exception ex)
                {
                    DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                    ErrorDTO er = new ErrorDTO();
                    er.PropertyName = "Error";
                    er.ErrorMessage = "tweet already exists";
                    Data.data = er;
                    return Request.CreateResponse(HttpStatusCode.Conflict, Data);
                }
            }
            else
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "Tweet masssage should be less than 240 and more than 1";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetTweet()
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            try
            {
                ITweetManager _TweetManger = BusinessFactory.GetTweetMangerObj();
                TweetsDTO TweetsDTO = _TweetManger.GetTweetByUser(ID);
                DataDTO<TweetsDTO> Data = new DataDTO<TweetsDTO>();
                Data.data = TweetsDTO;
                return Request.CreateResponse(HttpStatusCode.OK, Data);
            }
            catch (Exception ex)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = ex.Message;
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
        [Route("Vote/{Reaction}/{TweetID}")]
        [HttpPost]
        public HttpResponseMessage PostLike([FromUri] string Reaction, Guid TweetID)
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            try
            {
                ITweetManager _TweetManger = BusinessFactory.GetTweetMangerObj();
                bool isAdded = _TweetManger.AddVote(ID, TweetID, Reaction);
                if (isAdded) { return Request.CreateResponse(HttpStatusCode.OK); }
                else
                {
                    DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                    ErrorDTO er = new ErrorDTO();
                    er.PropertyName = "Error";
                    er.ErrorMessage = "reaction not found";
                    Data.data = er;
                    return Request.CreateResponse(HttpStatusCode.Conflict, Data);
                }

            }
            catch (Exception ex)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = ex.Message;
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
        [Route("Delete/{TweetID}")]
        [HttpDelete]
        public HttpResponseMessage DeleteTweet([FromUri]  Guid TweetID)
        {
            ITweetManager _TweetManger = BusinessFactory.GetTweetMangerObj();
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            try
            {
                
                _TweetManger.DeleteTweet(ID, TweetID);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = ex.Message;
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }

        }
        [Route("Edit/{TweetID}")]
        [HttpPut]
        public HttpResponseMessage EditTweet([FromBody] TweetModel tweetModel, [FromUri] Guid TweetID)
        {
            if (ModelState.IsValid)
            {
                ITweetManager _TweetManger = BusinessFactory.GetTweetMangerObj();
                object Token;
                Request.Properties.TryGetValue("mykey", out Token);
                Guid ID = new Guid(Token.ToString());
                try
                {
                    TweetDTO TweetDTO = _TweetManger.EditTweet(tweetModel, ID, TweetID);
                    DataDTO<TweetDTO> Data = new DataDTO<TweetDTO>();
                    Data.data = TweetDTO;
                    return Request.CreateResponse(HttpStatusCode.OK, Data);
                }
                catch (Exception ex)
                {
                    DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                    ErrorDTO er = new ErrorDTO();
                    er.PropertyName = "Error";
                    er.ErrorMessage = ex.Message;
                    Data.data = er;
                    return Request.CreateResponse(HttpStatusCode.Conflict, Data);
                }
            }
            else
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "tweeet is cannot blank ";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetAllSearchTweet([FromUri]  string searchstring)
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            ITweetManager _TweetManger = BusinessFactory.GetTweetMangerObj();
           
            try
            {
                TweetsDTO tweets = new TweetsDTO();
                tweets= _TweetManger.SearchHashTags(searchstring.ToLower());
                DataDTO<TweetsDTO> Data = new DataDTO<TweetsDTO>();
                Data.data = tweets;
                return Request.CreateResponse(HttpStatusCode.OK, Data);
            }
            catch (Exception ex)
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = ex.Message;
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
    }
}