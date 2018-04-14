namespace InShare.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Comment",
                c => new
                    {
                        CommentId = c.Long(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 50),
                        PostId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.T_User", t => t.UserId)
                .ForeignKey("dbo.T_Post", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 10),
                        FullName = c.String(nullable: false, maxLength: 20),
                        Biography = c.String(maxLength: 100),
                        IsPrivate = c.Boolean(nullable: false),
                        ProfilePic = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.T_UserProfile",
                c => new
                    {
                        ProfileId = c.Long(nullable: false),
                        Password = c.String(nullable: false, maxLength: 32),
                        PasswordSalt = c.String(nullable: false, maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 30),
                        PhoneNum = c.String(maxLength: 11),
                        LastPassword = c.String(maxLength: 32),
                        LastPasswordSalt = c.String(maxLength: 10),
                        LastLoginDateTime = c.DateTime(),
                        IP = c.String(maxLength: 15),
                        LastEditDateTime = c.DateTime(),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.T_User", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.T_Post",
                c => new
                    {
                        PostId = c.Long(nullable: false),
                        ShortCode = c.String(nullable: false, maxLength: 20),
                        Caption = c.String(maxLength: 100),
                        DisplayUrl = c.String(nullable: false),
                        IsVideo = c.Boolean(nullable: false),
                        UserId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.T_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_Tag",
                c => new
                    {
                        TagId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.T_Follow",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FollowId = c.Long(nullable: false),
                        FollowedId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_Liker",
                c => new
                    {
                        LikerId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        PostId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LikerId)
                .ForeignKey("dbo.T_Post", t => t.PostId)
                .ForeignKey("dbo.T_User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.T_Log",
                c => new
                    {
                        LogId = c.Long(nullable: false, identity: true),
                        LogType = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 100),
                        UserId = c.Long(nullable: false),
                        IP = c.String(nullable: false, maxLength: 40),
                        CreateDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.T_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_PostTags",
                c => new
                    {
                        PostId = c.Long(nullable: false),
                        TagId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.T_Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.T_Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Log", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_Liker", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_Liker", "PostId", "dbo.T_Post");
            DropForeignKey("dbo.T_Comment", "PostId", "dbo.T_Post");
            DropForeignKey("dbo.T_PostTags", "TagId", "dbo.T_Tag");
            DropForeignKey("dbo.T_PostTags", "PostId", "dbo.T_Post");
            DropForeignKey("dbo.T_Post", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_Comment", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_UserProfile", "ProfileId", "dbo.T_User");
            DropIndex("dbo.T_PostTags", new[] { "TagId" });
            DropIndex("dbo.T_PostTags", new[] { "PostId" });
            DropIndex("dbo.T_Log", new[] { "UserId" });
            DropIndex("dbo.T_Liker", new[] { "PostId" });
            DropIndex("dbo.T_Liker", new[] { "UserId" });
            DropIndex("dbo.T_Post", new[] { "UserId" });
            DropIndex("dbo.T_UserProfile", new[] { "ProfileId" });
            DropIndex("dbo.T_Comment", new[] { "UserId" });
            DropIndex("dbo.T_Comment", new[] { "PostId" });
            DropTable("dbo.T_PostTags");
            DropTable("dbo.T_Log");
            DropTable("dbo.T_Liker");
            DropTable("dbo.T_Follow");
            DropTable("dbo.T_Tag");
            DropTable("dbo.T_Post");
            DropTable("dbo.T_UserProfile");
            DropTable("dbo.T_User");
            DropTable("dbo.T_Comment");
        }
    }
}
