using AutoMapper;
using Nagarro.Gitter.Entites.Entity;
using Nagarro.Gitter.Shared.DTO;
using Nagarro.Gitter.Shared.ModelBasic;
using Nagarro.Gitter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Gitter.Shared
{
    public class MapperFactory
    {
        public static IMapper GetMapperObj()
        {
            var Config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserModel, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<UserBasic, UserDTO>();
                cfg.CreateMap<UserModel, UserBasic>();
                cfg.CreateMap<UserBasic, User>();
                cfg.CreateMap<Tweet, TweetDTO>();
                cfg.CreateMap<UserTweetVote, UserTweetVotesDTO>();
            });
            return new Mapper(Config);
           
        }
    }
}
