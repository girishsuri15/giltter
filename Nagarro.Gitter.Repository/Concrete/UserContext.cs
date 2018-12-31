using AutoMapper;
using Nagarro.Gitter.Entites.Concerte;
using Nagarro.Gitter.Entites.Contract;
using Nagarro.Gitter.Entites.Entity;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Repository.Exceptions;
using Nagarro.Gitter.Shared;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.ModelBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository.Concrete
{
   public class UserContext:IUser
    {
       private readonly GitterContext _dbContext;
       public UserContext()
        {
            _dbContext = EntitesFactory.GetEntitesObj(); 
        }

       public UserDTO Add(UserBasic userBasic)
        {
            IMapper Mapper = MapperFactory.GetMapperObj();
            User user = Mapper.Map<UserBasic, User>(userBasic);
            user.ID = Guid.NewGuid();
            user.CreationDate = System.DateTime.Now;
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                return userDTO;
            }
            catch (Exception)
            {
                throw new UserCannotCreate();
            }
        }


       public UserDTO GetUser(Guid ID)
        {
            User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
            if (user == null)
            {
                throw new Exception();
            }
            IMapper Mapper = MapperFactory.GetMapperObj();
            UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
        public UserBasic GetUserByEmail(string Email)
        {

           User user = _dbContext.Users.Where(u=>u.Email==Email).FirstOrDefault();
            if (user == null)
            {
                throw new Exception();
            }
            IMapper Mapper = MapperFactory.GetMapperObj();
            UserBasic userBasic = Mapper.Map<User, UserBasic>(user);
            return userBasic;
        }
        public bool ValidateUser(Guid Id)
        {
            User user = _dbContext.Users.Where(u => u.ID == Id).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;

        }
        public UsersDTO GetAllSearchUser(Guid ID,string searchstring)
        {
            IEnumerable<User> users = _dbContext.Users.Where(u => u.UserName.ToLower().Contains(searchstring.ToLower()) || u.Email.ToLower().Contains(searchstring.ToLower())).ToList();
            UsersDTO usersDTO = new UsersDTO();
            IMapper _Mapper = MapperFactory.GetMapperObj();
            usersDTO.Users = _Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return usersDTO;
        }
    }
}
