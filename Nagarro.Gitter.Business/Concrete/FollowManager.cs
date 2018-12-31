using Nagarro.Gitter.Business.Contract;
using Nagarro.Gitter.Repository.Contract;
using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business.Concrete
{
   public class FollowManager: IFollowManager
    {
        private readonly IFollowContext _follow;
        public FollowManager()
        {
            _follow = RepositoryFactory.GetFollowContextObj();
        }
        public bool AddFollowing(Guid ID, Guid Following)
        {
            if (ID == Following)
            {
                return false;
            }
            return _follow.AddFollowing(ID, Following);
        }
       public void RemoveFollowing(Guid ID, Guid Following)
        {
          bool isRemoved = _follow.RemoveFollowing(ID, Following);
            if (!isRemoved)
            {
                throw new Exception();
            }
        }
       public int CountFollower(Guid ID)
        {
           return _follow.CountFollower(ID);
        }
      public  int CountFollowing(Guid ID)
        {
            return _follow.CountFollowing(ID);
        }
       public UsersDTO UserFollower(Guid ID)
        {
            try
            {
                return _follow.UserFollower(ID);
            }
            catch (Exception) {
                return new UsersDTO();
            }
        }
     public   UsersDTO UserFollowing(Guid ID)
        {
            try { return _follow.UserFollowing(ID); }

            catch (Exception)
            {
                return new UsersDTO();
            }
        }
    }
}
