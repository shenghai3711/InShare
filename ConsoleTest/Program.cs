using InShare.IService;
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
            IUserService userService = new UserService();
            long id = userService.Add("lhppppp", "LHP", "123456");

            //IPostService postService = new PostService();
            //long postId = postService.Add(6502458435, "#123 #test askdjfla;sdf", "1.png");

            //Console.WriteLine(postId);

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
