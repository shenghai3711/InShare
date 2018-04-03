﻿using InShare.Model;
using System.Collections.Generic;

namespace InShare.IService
{
    public interface IUserService : IServiceSupport
    {
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="userName">账号</param>
        /// <param name="fullName">全名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        long Add(string email, string userName, string fullName, string password);

        /// <summary>
        /// 编辑用户资料
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="userName">账号</param>
        /// <param name="fullName">全名</param>
        /// <param name="bio">介绍</param>
        /// <param name="isPrivate">是否私有</param>
        /// <param name="genger">性别</param>
        /// <param name="profilePic">头像</param>
        /// <returns></returns>
        bool Edit(long userId, string userName, string fullName, string bio, bool isPrivate, bool genger, string email);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pwd">新密码</param>
        /// <param name="oldPwd">旧密码</param>
        /// <returns></returns>
        bool UpdatePwd(long userId, string pwd, string oldPwd);

        /// <summary>
        /// 根据用户编号查找用户
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        UserEntity GetUserById(long userId);

        /// <summary>
        /// 根据账号查找用户
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns></returns>
        UserEntity GetUserByUserName(string userName);

        /// <summary>
        /// 根据用户名称查找用户
        /// </summary>
        /// <param name="name">名称（账号/全名）</param>
        /// <returns></returns>
        List<UserEntity> GetUserByName(string name);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        List<UserEntity> GetAllUserList();

        /// <summary>
        /// 获取所有用户数量
        /// </summary>
        /// <returns></returns>
        long GetAllUserCount();

        /// <summary>
        /// 根据用户编号删除用户
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        bool DeleteUser(long userId);

        /// <summary>
        /// 根据用户编号批量删除用户
        /// </summary>
        /// <param name="userIds">用户编号数组</param>
        /// <returns></returns>
        bool BatchDelete(long[] userIds);
    }
}
