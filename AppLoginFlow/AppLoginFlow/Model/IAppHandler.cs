using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppLoginFlow.Model
{
    public interface IAppHandler
    {
        Task<bool> LaunchApp(string uri);

    }
}
