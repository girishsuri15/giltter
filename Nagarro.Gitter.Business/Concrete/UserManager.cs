using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Shared;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.ModelBasic;
using Nagarro.Gitter.Shared.Models;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nagarro.Gitter.Business.Exceptions;

namespace Nagarro.Gitter.Business.Concrete
{
   public class UserManager:IUserManager
    {
        IUser _userContext;
        public UserManager()
        {
            _userContext = RepositoryFactory.GetUserContextObj();
        }
        public UserDTO GetUser(Guid ID)
        {
            return _userContext.GetUser(ID);
            
        }
        public UserDTO CreateUser(UserBasic userBasic)
        {
            return _userContext.Add(userBasic);
        }
        public UserDTO AuthenticateUser(UserLoginModel userLogin)
        {
            try
            {
                UserBasic user = _userContext.GetUserByEmail(userLogin.Email);
                bool isUserAuth = PasswordHelper.VerifyPassword(userLogin.Password, user.Hash);
                if (isUserAuth)
                {
                    IMapper Mapper = MapperFactory.GetMapperObj();
                    UserDTO userDTO = Mapper.Map<UserBasic, UserDTO>(user);
                    return userDTO;
                }
                else
                {
                    throw new UserPasswordInCorrectException();
                }
            }
            catch (Exception)
            {
                throw new UserNotFound();
            }
            
        }
        public bool ValidateUser(Guid Id)
        {
            return _userContext.ValidateUser(Id);
        }
       public UsersDTO GetAllSearchUser(Guid ID, string searchstring)
        {
            return _userContext.GetAllSearchUser(ID,searchstring);
        }
    }
}
