using InShare.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service
{
    public class InShareContext : DbContext
    {
        public InShareContext() : base("name=dbContext")
        {
            this.Database.Log = (sql) =>
            {
                _log.DebugFormat("EF执行SQL：{0}", sql);
            };
            //System.Data.Entity.Database.SetInitializer<InShareContext>(null);
        }

        private static ILog _log = LogManager.GetLogger(typeof(InShareContext));

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<LikerEntity> Likers { get; set; }
        public DbSet<UserProfileEntity> Profiles { get; set; }
        public DbSet<FollowEntity> Follows { get; set; }
        public DbSet<VerifyEntity> Verifys { get; set; }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //读取配置（Config）
            //从程序集中加载所有EntityTypeConfiguration的子类到配置中
            //加载形式：反射
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
