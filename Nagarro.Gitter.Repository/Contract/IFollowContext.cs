using Nagarro.Gitter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository.Contract
{
    public interface IFollowContext
    {
        bool AddFollowing(Guid ID, Guid Following);
        bool RemoveFollowing(Guid ID, Guid Following);
        int CountFollower(Guid ID);
        int CountFollowing(Guid ID);
        UsersDTO UserFollower(Guid ID);
        UsersDTO UserFollowing(Guid ID);
    }
}
