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
            long id = userService.Add("123456@qq.com", "instagram", "Instagrm", "123456");

            Console.WriteLine(id);

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
