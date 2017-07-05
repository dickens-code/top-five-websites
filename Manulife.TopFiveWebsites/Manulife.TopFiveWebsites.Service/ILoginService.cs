using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Service
{
    public interface ILoginService
    {
        User Login(string userId, string password);
    }
}
