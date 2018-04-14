﻿using InShare.IService;
using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service
{
    public class FollowService : IFollowService
    {
        public bool Follow(long userId, long followedId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> baseService = new BaseService<FollowEntity>(db);
                var follow = baseService.GetAll().Where(f => f.FollowId == userId && f.FollowedId == followedId).SingleOrDefault();
                if (follow == null)
                    db.Follows.Add(new FollowEntity { FollowId = userId, FollowedId = followedId });
                else
                    follow.IsDeleted = false;
                db.SaveChanges();
                return true;
            }
        }

        public int GetFollowerCount(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> baseService = new BaseService<FollowEntity>(db);
                return baseService.GetAll().Where(f => f.FollowedId == userId).Count();
            }
        }

        public List<long> GetFollowerPagerList(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> baseService = new BaseService<FollowEntity>(db);
                return baseService.GetAll().Where(f => f.FollowedId == userId).Skip(pageSize * pageIndex).Take(pageSize).Select(f => f.FollowId).ToList();
            }
        }

        public int GetFollowingCount(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> baseService = new BaseService<FollowEntity>(db);
                return baseService.GetAll().Where(f => f.FollowId == userId).Count();
            }
        }

        public List<long> GetFollowingPagerList(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> baseService = new BaseService<FollowEntity>(db);
                return baseService.GetAll().Where(f => f.FollowId == userId).Skip(pageSize * pageIndex).Take(pageSize).Select(f => f.FollowedId).ToList();
            }
        }

        public bool Unfollow(long userId, long unfollowId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> baseService = new BaseService<FollowEntity>(db);
                var follow = baseService.GetAll().Where(f => f.FollowId == userId && f.FollowedId == unfollowId).SingleOrDefault();
                if (follow != null)
                    follow.IsDeleted = true;
                else
                    return false;
                db.SaveChanges();
                return true;
            }
        }
    }
}