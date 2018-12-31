using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.ModelBasic;
using Nagarro.Gitter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Business.Contract
{
    public interface IUserManager
    {
        UserDTO GetUser(Guid ID);
        UserDTO CreateUser(UserBasic userBasic);
        UserDTO AuthenticateUser(UserLoginModel userLogin);
         bool ValidateUser(Guid Id);
       UsersDTO GetAllSearchUser(Guid ID, string searchstring);
    }
}
