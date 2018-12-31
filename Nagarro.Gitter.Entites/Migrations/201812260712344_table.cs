namespace Nagarro.Gitter.Entites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HashTags",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        HashTagDescription = c.String(maxLength: 50),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.HashTagDescription, unique: true);
            
            CreateTable(
                "dbo.TweetHashTagMaps",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        HashTagID = c.Guid(nullable: false),
                        TweetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HashTags", t => t.HashTagID, cascadeDelete: true)
                .ForeignKey("dbo.Tweets", t => t.TweetID, cascadeDelete: true)
                .Index(t => t.HashTagID)
                .Index(t => t.TweetID);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tweet_Descrption = c.String(nullable: false, maxLength: 240),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Email = c.String(maxLength: 50),
                        Hash = c.String(),
                        UserName = c.String(),
                        Image = c.Binary(),
                        Country = c.String(),
                        ContactNumber = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.UserTweetVotes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Vote = c.Int(nullable: false),
                        TweetID = c.Guid(nullable: false),
                        Users_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tweets", t => t.TweetID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_ID)
                .Index(t => t.TweetID)
                .Index(t => t.Users_ID);
            
            CreateTable(
                "dbo.UserFollowerMaps",
                c => new
                    {
                        Follower = c.Guid(nullable: false),
                        Following = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Follower, t.Following })
                .ForeignKey("dbo.Users", t => t.Follower)
                .ForeignKey("dbo.Users", t => t.Following)
                .Index(t => t.Follower)
                .Index(t => t.Following);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTweetVotes", "Users_ID", "dbo.Users");
            DropForeignKey("dbo.UserTweetVotes", "TweetID", "dbo.Tweets");
            DropForeignKey("dbo.Tweets", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserFollowerMaps", "Following", "dbo.Users");
            DropForeignKey("dbo.UserFollowerMaps", "Follower", "dbo.Users");
            DropForeignKey("dbo.TweetHashTagMaps", "TweetID", "dbo.Tweets");
            DropForeignKey("dbo.TweetHashTagMaps", "HashTagID", "dbo.HashTags");
            DropIndex("dbo.UserFollowerMaps", new[] { "Following" });
            DropIndex("dbo.UserFollowerMaps", new[] { "Follower" });
            DropIndex("dbo.UserTweetVotes", new[] { "Users_ID" });
            DropIndex("dbo.UserTweetVotes", new[] { "TweetID" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Tweets", new[] { "UserID" });
            DropIndex("dbo.TweetHashTagMaps", new[] { "TweetID" });
            DropIndex("dbo.TweetHashTagMaps", new[] { "HashTagID" });
            DropIndex("dbo.HashTags", new[] { "HashTagDescription" });
            DropTable("dbo.UserFollowerMaps");
            DropTable("dbo.UserTweetVotes");
            DropTable("dbo.Users");
            DropTable("dbo.Tweets");
            DropTable("dbo.TweetHashTagMaps");
            DropTable("dbo.HashTags");
        }
    }
}
