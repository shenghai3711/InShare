using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.IService
{
    public interface IAdminUserService : IServiceSupport
    {
        bool Login(string userName, string password);

    }
}
