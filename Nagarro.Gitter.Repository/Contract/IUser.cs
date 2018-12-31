using Nagarro.Gitter.Entites.Entity;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.ModelBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Repository.Contract
{
   public interface IUser
    {

        UserDTO Add(UserBasic userBasic);
           UserDTO GetUser(Guid ID);
        UserBasic GetUserByEmail(string Email);
        bool ValidateUser(Guid Id);
        UsersDTO GetAllSearchUser(Guid ID,string searchstring);

    }
}
