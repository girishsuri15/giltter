using AutoMapper;
using Nagarro.Gitter.Entites.Concerte;
using Nagarro.Gitter.Entites.Entity;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Shared;
using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository.Concrete
{
   public class FollowContext:IFollowContext
    {
        private readonly GitterContext _dbContext;
        private readonly IMapper _Mapper;
        public FollowContext()
        {
            _dbContext = EntitesFactory.GetEntitesObj();
            _Mapper = MapperFactory.GetMapperObj();
        }
      public  bool AddFollowing(Guid ID, Guid Following)
        {
            try
            {
                User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();

                User Followinguser = _dbContext.Users.Where(u => u.ID == Following).FirstOrDefault();
                if (Followinguser == null)
                {
                    return false;
                }
                foreach (User userFollower in user.Follower)
                {
                    if(userFollower.ID== Followinguser.ID)
                    {

                        return true;
                    }
                }
               
                user.Follower.Add(Followinguser);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
      public  bool RemoveFollowing(Guid ID, Guid Following)
        {
            User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();

            User Followinguser = _dbContext.Users.Where(u => u.ID == Following).FirstOrDefault();
            if (Followinguser == null)
            {
                return false;
            }
            bool UserFound = false;
            foreach (User userFollower in user.Follower)
            {
                if (userFollower.ID == Followinguser.ID)
                {

                    UserFound = true;
                }
            }
            if (UserFound)
            {
                user.Follower.Remove(Followinguser);
                _dbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public int CountFollowing(Guid ID)
        {
            User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
            return user.Follower.Count();
        }
       public int CountFollower(Guid ID)
        {
            User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
            return user.Following.Count();
        }
       public UsersDTO UserFollowing(Guid ID)
        {
            try
            {
                User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
                List<User> following = new List<User>();
                foreach (User Following in user.Follower)
                {
                    following.Add(Following);
                }
                UsersDTO usersDTO = new UsersDTO();
                usersDTO.Users = _Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(following);
                return usersDTO;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
      public UsersDTO UserFollower(Guid ID)
        {
            try
            {
                User user = _dbContext.Users.Where(u => u.ID == ID).FirstOrDefault();
                List<User> follower = new List<User>();
                foreach (User Following in user.Following)
                {
                    follower.Add(Following);
                }
                UsersDTO usersDTO = new UsersDTO();
                usersDTO.Users = _Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(follower);
                return usersDTO;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

    }
}
