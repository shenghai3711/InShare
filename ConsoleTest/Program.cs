using InShare.Common;
using InShare.IService;
using InShare.Model;
using InShare.Service;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UploadImage();
            //bool b = EmailHelper.SendMail(new Email { DisplayName = "朱生海", Body = "测试邮件发送", Subject = "123456" }, "zhushenghai@outlook.com");

            Console.WriteLine("OK");
            Console.ReadKey();
        }

        static void UploadImage()
        {
            #region 上传图片

            //string str = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAeYAAAIlCAMAAAA32at6AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAAZQTFRF////AAAAVcLTfgAAAAF0Uk5TAEDm2GYAAAEcSURBVHja7MExAQAAAMKg9U9tCU+gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAG4mwAAUnwABSto4TAAAAABJRU5ErkJggg==";

            //using (var stream = ImgBase64ToStream(str))
            //{
            //    var img = System.Drawing.Image.FromStream(stream);
            //    img.Save(@"C:\Users\ZhuSh\Desktop\" + "logo.png");
            //}
            

            ////CompressImgBase64Upload(str, "456789.jpg");

            string url1 = ImageHelper.UploadFile(@"C:\Users\ZhuSh\Desktop\QQ图片20180430233640.jpg", ".png");
            string url2 = ImageHelper.UploadFile(@"C:\Users\ZhuSh\Desktop\QQ图片20180430233646.jpg", ".png");
            string url3 = ImageHelper.UploadFile(@"C:\Users\ZhuSh\Desktop\QQ图片20180430233624.jpg", ".png");
            string url4 = ImageHelper.UploadFile(@"C:\Users\ZhuSh\Desktop\QQ图片20180430233633.jpg", ".png");

            Console.WriteLine("OK");
            #endregion
        }

        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        private static Stream ImgBase64ToStream(string base64str)
        {
            try
            {
                var imgData = base64str.Split(',')[1];
                //过滤特殊字符即可   
                string dummyData = imgData.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
                if (dummyData.Length % 4 > 0)
                {
                    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                }
                byte[] byteArray = Convert.FromBase64String(dummyData);
                return new System.IO.MemoryStream(byteArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static bool CompressImgBase64Upload(string base64str, string saveName)
        {
            try
            {
                var imgData = base64str.Split(',')[1];
                //过滤特殊字符即可   
                string dummyData = imgData.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
                if (dummyData.Length % 4 > 0)
                {
                    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                }
                byte[] byteArray = Convert.FromBase64String(dummyData);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray))
                {
                    var img = System.Drawing.Image.FromStream(ms);
                    //var path = "/Upload/Image/";
                    //var uploadpath = Server.MapPath("~" + path);
                    //if (!Directory.Exists(uploadpath))
                    //{
                    //    Directory.CreateDirectory(uploadpath);
                    //}
                    img.Save(@"C:\Users\ZhuSh\Desktop\" + saveName);
                    return true;
                    //return Json(new { Success = true, FilePath = path + saveName, Message = "上传成功！" });
                }
            }
            catch (Exception e)
            {
                return false;
                //return Json(new { Success = false, FilePath = "", Message = "出错了！" });
            }
        }

        static void CreateData()
        {
            string biography = "Why not live a life of your dreams?";
            string profilePic = "https://scontent-hkg3-2.cdninstagram.com/vp/761993dc007e86f9968a9f938e80e70a/5B697B1A/t51.2885-19/s150x150/25005602_1904421042944210_7172550441782214656_n.jpg";

            IUserService userService = new UserService();
            var admin = userService.GetUserById(userService.Add("admin", "管理员", "123456"));
            //userService.Edit(admin.Id, admin.UserName, admin.FullName, biography, false, true, profilePic);
            List<UserEntity> userList = new List<UserEntity>() { admin };
            IFollowService followService = new FollowService();
            IPostService postService = new PostService();

            #region PostDic
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"#IGCD |#sichuan |#shanghai |#guangzhou |#china |#beijing |#chinese |#hongkong |#hotpot |#上海 |#重庆 |#广州 |#成都 | #北京 | #四川 | #英国 |#中国 |#chongqing","https://scontent-hkg3-2.cdninstagram.com/vp/81fbc5469b0214b401df906188a9d203/5B5E989F/t51.2885-15/e35/30076807_185022462128987_2634373999806644224_n.jpg" },
                { @"Photo illustration by karencantuq
After a few weeks of sickness, Lily the golden retriever is finally back to her usual antics. “When we left the vet, you could see her excitement,” says her human, visual artist Karen Cantu Quintanilla(karencantuq). “She ran toward these plants and started playing around, getting all muddy.My heart smiled, watching her being herself.” 💗🐞
Follow along to see more of our favorites from last weekend’s hashtag project, #WHPwildthing.","https://scontent-hkg3-2.cdninstagram.com/vp/e38c381e89e706ef9635e33db6e16dbc/5B68BF71/t51.2885-15/e35/30591615_881741418699920_600203219846561792_n.jpg"},
                { @"Photo by masoudoroudian
Hanging out with a few cool cats in Tehran 🐾 #WHPwildthing","https://scontent-hkg3-2.cdninstagram.com/vp/5a6c4d5c4160f1f8b59a58a4c7b98433/5B592BDC/t51.2885-15/e35/30590576_118389625684611_4178553869594263552_n.jpg"},
                { @"Photo by reverieroaming
“Some days you just gotta swap the office for the mountains and the Wi-Fi for the woods and go on a hike with your best friend,” writes Amy Lynn (reverieroaming) in her caption. #WHPwildthing","https://scontent-hkg3-2.cdninstagram.com/vp/e49d5ac5eaa8758fa234c741c36f73af/5B71CBC5/t51.2885-15/e35/30087512_587995028236388_4322332280694505472_n.jpg"},
                { @"Photo by michellevalbergphotography
Jumping for joy at the arrival of spring snow. 😍
Follow along to see more of our favorites from last weekend’s hashtag project, #WHPwildthing.","https://scontent-hkg3-2.cdninstagram.com/vp/33b9c5548e1f16fed5da48fc58ed828e/5B609D4A/t51.2885-15/e35/30084252_181096276041718_5268656572644458496_n.jpg"},
                { "I am drawn to combining the sad with the beautiful","https://scontent-hkg3-2.cdninstagram.com/vp/850789af95b9ca6167583d28fbddfb9a/5B73E8EE/t51.2885-15/e35/30087720_114361546087186_3830904802745778176_n.jpg"},
                {@"_mijini 
标记我分享你的自拍，让更多人看到你
#katy #perry #katyperry #pretty #beautiful #music #lovethissong #kp #katykats #katykat #katycats #katycat #caligirls #californiagirls #partofme #smile #instagood #instaperry #love #photooftheday #extraterrestrial #teenagedream #wideawake","https://scontent-hkg3-2.cdninstagram.com/vp/a3323727d7d2cb514106c15c72080617/5B652D05/t51.2885-15/e35/21980719_147546825852833_1269593333424979968_n.jpg" },
                { @"jhlii 
标记我分享你的自拍，让更多人看到你
#katy #perry #katyperry #pretty #beautiful #music #lovethissong #kp #katykats #katykat #katycats #katycat #caligirls #californiagirls #partofme #smile #instagood #instaperry #love #photooftheday #extraterrestrial #teenagedream #wideawake","https://scontent-hkg3-2.cdninstagram.com/vp/770dae4e2f8ad2dddce8aa831ec2d82b/5B5526CB/t51.2885-15/e35/21980601_133534460719251_2834026624008060928_n.jpg"},
                { @"dj_siena 
标记我分享你的自拍，让更多人看到你
#katy #perry #katyperry #pretty #beautiful #music #lovethissong #kp #katykats #katykat #katycats #katycat #caligirls #californiagirls #partofme #smile #instagood #instaperry #love #photooftheday #extraterrestrial #teenagedream #wideawake","https://scontent-hkg3-2.cdninstagram.com/vp/62489e09ff9e0236c2166265c4322c6f/5B5DD01B/t51.2885-15/e35/21879373_1435036036615296_7736067429670846464_n.jpg"},
                { @"@_bebe0519_ 
标记我分享你的自拍，让更多人看到你
#happy #smile #fun #instahappy #goodmood #sohappy #happier #excited #feelgood #smiling #funtimes #funny #feliz #feelgood #feelgoodphoto #joy #happyhappy #enjoy #love #lovelife #instagood #laugh #laughing","https://scontent-hkg3-2.cdninstagram.com/vp/0e03427b57ab6e76d63c2552b2572226/5B5F0E47/t51.2885-15/e35/21827267_133356903961013_3484997796107386880_n.jpg"},
                { @"irisirisss90 
标记我分享你的自拍，让更多人看到你
#fashion #style #stylish #love #me #cute #photooftheday #nails #hair #beauty #beautiful #instagood #instafashion #pretty #girly #pink #girl #girls #eyes #model #dress #skirt #shoes #heels #styles #outfit #purse #jewlery#shopping","https://scontent-hkg3-2.cdninstagram.com/vp/c69904bcd3e5ec829598e5576643e029/5B6D6E97/t51.2885-15/e35/21827011_533848550292213_537823935178211328_n.jpg"},
                { @"adelekok 
标记我分享你的自拍，让更多人看到你
#fashion #style #stylish #love #me #cute #photooftheday #nails #hair #beauty #beautiful #instagood #instafashion #pretty #girly #pink #girl #girls #eyes #model #dress #skirt #shoes #heels #styles #outfit #purse #jewlery#shopping","https://scontent-hkg3-2.cdninstagram.com/vp/ad6ace62d9bc8d8299d629309a8d5e40/5B66DB35/t51.2885-15/e35/21878934_381924075559203_5692128743028424704_n.jpg"},
                { @"@babebani 
标记我分享你的自拍，让更多人看到你
#fashion #style #stylish #love #me #cute #photooftheday #nails #hair #beauty #beautiful #instagood #instafashion #pretty #girly #pink #girl #girls #eyes #model #dress #skirt #shoes #heels #styles #outfit #purse #jewlery#shopping","https://scontent-hkg3-2.cdninstagram.com/vp/ff246d161ff3cac22b0d2776a22842aa/5B5FC0D6/t51.2885-15/e35/21569404_1931782723706365_6424639826292637696_n.jpg"},
                { @"_reiikoyuii
标记我分享你的自拍，让更多人看到你
#pretty #picstitch #tweegram #my #hair #jj #bored #life #swag #cool #funny #igdaily #family #repost #photo #pink #amazing #blue #girls #hot #中国","https://scontent-hkg3-2.cdninstagram.com/vp/9a7abc5abda261c3dd922aec1e10a2a0/5B62C7EB/t51.2885-15/e35/21434220_1427843753978754_1111383342583906304_n.jpg"},
                { @"tame__c 
标记我分享你的自拍，让更多人看到你
#pretty #picstitch #tweegram #my #hair #jj #bored #life #swag #cool #funny #igdaily #family #repost #photo #pink #amazing #blue #girls #hot","https://scontent-hkg3-2.cdninstagram.com/vp/edbfcf9ba4ea4306d25f18e2ef42f79a/5B50D478/t51.2885-15/e35/21371745_316477285484968_5420867168482885632_n.jpg"}
            };
            #endregion
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();
            for (int i = 0; i < 100; i++)
            {
                var user = userService.GetUserById(userService.Add("test" + i, "Test" + i, "123456"));
                //userService.Edit(user.Id, user.UserName, user.FullName, biography, false, true, profilePic);
                foreach (var item in dic)
                {
                    var post = postService.GetPostInfo(postService.Add(user.Id, item.Key.Length > 100 ? item.Key.Substring(0, 99) : item.Key, item.Value, "重庆"));
                    Console.WriteLine(string.Format("{0}发贴成功", user.UserName));
                    foreach (var userItem in userList)
                    {
                        commentService.Add(userItem.Id, post.Id, "Why not live a life of your dreams?");
                        likeService.Like(userItem.Id, post.Id);
                        Console.WriteLine(string.Format("{0}评论点赞成功", userItem.UserName));
                    }
                    //Thread.Sleep((int)RandomHelper.CreateId(5));
                }
                foreach (var item in userList)
                {
                    followService.Follow(user.Id, item.Id);
                }
                followService.Follow(user.Id, admin.Id);
                userList.Add(user);
                Console.WriteLine(string.Format("{0}用户已创建", user.UserName));
            }

            //IPostService postService = new PostService();
            //long postId = postService.Add(6502458435, "#123 #test askdjfla;sdf", "1.png");

            //Console.WriteLine(postId);

        }
    }
}
