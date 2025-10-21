using Etic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.Services
{
    public interface ILoginService
    {
        User Login(string email,string password);
    }
}
