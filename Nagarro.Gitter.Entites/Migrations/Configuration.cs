namespace Nagarro.Gitter.Entites.Migrations
{
    using Nagarro.Gitter.Entites.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Nagarro.Gitter.Entites.Concerte.GitterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Nagarro.Gitter.Entites.Concerte.GitterContext context)
        {

            //User u = new User { ID = Guid.NewGuid(), UserName = "girish", Email = "girish15suri@gmail.com", CreationDate = System.DateTime.Now };
            //User u1 = new User { ID = Guid.NewGuid(), UserName = "girish1", Email = "girish151suri@gmail.com", CreationDate = System.DateTime.Now };
            //User u2 = new User { ID = Guid.NewGuid(), UserName = "girish2", Email = "girish152suri@gmail.com", CreationDate = System.DateTime.Now };
            //User u3 = new User { ID = Guid.NewGuid(), UserName = "girish3", Email = "girish153suri@gmail.com", CreationDate = System.DateTime.Now };
            //List<User> fo = new List<User>();
            //fo.Add(u1);
            //fo.Add(u2);
            //u.Follower = fo;
            ////UserFollowerMap uf = new UserFollowerMap {ID = Guid.NewGuid(), Follower = u.ID, Following=u1.ID };
            ////UserFollowerMap uf1 = new UserFollowerMap { ID = Guid.NewGuid(), Follower = u.ID, Following = u2.ID };
            ////UserFollowerMap uf2 = new UserFollowerMap { ID = Guid.NewGuid(), Follower = u1.ID, Following = u.ID };
            //context.Users.AddOrUpdate(u, u1, u2, u3);
            //context.UserFollowerMaps.AddOrUpdate(uf, uf1);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
