using AutoMapper;
using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Shared.Models;
using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nagarro.Gitter.Shared;
using Nagarro.Gitter.Shared.ModelBasic;
using Nagarro.Gitter.Presentation.ActionFilter;

namespace Nagarro.Gitter.Presentation.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage LoginUser([FromBody] UserLoginModel userLogin)
        {
            if (ModelState.IsValid)
            {
                IUserManager _userManger = BusinessFactory.GetUserMangerObj();
                try
                {
                    UserDTO user = _userManger.AuthenticateUser(userLogin);
                    DataDTO<UserDTO> Data = new DataDTO<UserDTO>();
                    Data.data = user;
                    return Request.CreateResponse(HttpStatusCode.OK, Data);
                }
                catch (Exception)
                {
                    DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                    ErrorDTO er = new ErrorDTO();
                    er.PropertyName = "Error";
                    er.ErrorMessage = "User Not exist";
                    Data.data = er;
                    return Request.CreateResponse(HttpStatusCode.Conflict, Data);
                }

            }
            else
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "Provide valid User INformation";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }

        [HttpPost]
        public HttpResponseMessage RegisterUser([FromBody] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                IUserManager _userManger = BusinessFactory.GetUserMangerObj();
                IMapper Mapper = MapperFactory.GetMapperObj();
                UserBasic user = Mapper.Map<UserModel, UserBasic>(userModel);
                user.Hash = PasswordHelper.HashPassword(userModel.Password);
                try
                {
                    UserDTO userDTO = _userManger.CreateUser(user);
                    DataDTO<UserDTO> Data = new DataDTO<UserDTO>();
                    Data.data = userDTO;
                    return Request.CreateResponse(HttpStatusCode.Created, Data);
                }
                catch (Exception)
                {
                    DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                    ErrorDTO er = new ErrorDTO();
                    er.PropertyName = "Error";
                    er.ErrorMessage = "User already exists";
                    Data.data = er;
                    return Request.CreateResponse(HttpStatusCode.Conflict, Data);
                }
            }
            else
            {
                DataDTO<ErrorDTO> Data = new DataDTO<ErrorDTO>();
                ErrorDTO er = new ErrorDTO();
                er.PropertyName = "Error";
                er.ErrorMessage = "Cannot create User";
                Data.data = er;
                return Request.CreateResponse(HttpStatusCode.Conflict, Data);
            }
        }
        [UserAuthFilter]
        [HttpGet]
        public HttpResponseMessage GetUser()
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            IUserManager _userManger = BusinessFactory.GetUserMangerObj();
            try
            {
                UserDTO userDTO = _userManger.GetUser(ID);
                DataDTO<UserDTO> Data = new DataDTO<UserDTO>();
                Data.data = userDTO;
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
        [UserAuthFilter]
        [HttpGet]
        public HttpResponseMessage GetAllSearchUser([FromUri]  string searchstring)
        {
            object Token;
            Request.Properties.TryGetValue("mykey", out Token);
            Guid ID = new Guid(Token.ToString());
            IUserManager _userManger = BusinessFactory.GetUserMangerObj();
            try
            {
                UsersDTO usersDTO = _userManger.GetAllSearchUser(ID,searchstring);
                DataDTO<UsersDTO> Data = new DataDTO<UsersDTO>();
                Data.data = usersDTO;
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
