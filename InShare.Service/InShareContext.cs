﻿using InShare.Model;
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
        public InShareContext() : base("name=配置数据库名称")
        {
            Database.SetInitializer<InShareContext>(null);

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<LikerEntity> Likers { get; set; }
        public DbSet<UserProfileEntity> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //读取配置（Config）
            //从程序集中加载所有EntityTypeConfiguration的子类到配置中
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
