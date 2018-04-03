using InShare.Model;
using InShare.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (InShareContext db = new InShareContext())
            {
                UserEntity user1 = new UserEntity
                {
                    Id = 12345678,
                    UserName = "Test1",
                    FullName = "test1",
                    ProfilePic = "1.png",
                };
                db.Users.Add(user1);
                UserEntity user2 = new UserEntity
                {
                    Id = 12345679,
                    UserName = "Test1",
                    FullName = "test1",
                    ProfilePic = "1.png",
                };
                db.Users.Add(user2);
                UserEntity user3 = new UserEntity
                {
                    Id = 12345670,
                    UserName = "Test1",
                    FullName = "test1",
                    ProfilePic = "1.png",
                };
                db.Users.Add(user3);

                FollowEntity followEntity1 = new FollowEntity
                {
                    FollowId = 12345678,
                    FollowedId = 12345679
                };
                FollowEntity followEntity2 = new FollowEntity
                {
                    FollowId = 12345678,
                    FollowedId = 12345670
                };
                FollowEntity followEntity3 = new FollowEntity
                {
                    FollowId = 12345670,
                    FollowedId = 12345678
                };
                db.Follows.AddRange(new List<FollowEntity> { followEntity1, followEntity2, followEntity3 });
                //UserProfileEntity userProfile = new UserProfileEntity
                //{
                //    Id = 12345678,
                //    Email = "123456789@qq.com",
                //    Gender = true,
                //    Password = "12345678",
                //    PasswordSalt = "123456",
                //};
                //db.Profiles.Add(userProfile);
                db.SaveChanges();
            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
