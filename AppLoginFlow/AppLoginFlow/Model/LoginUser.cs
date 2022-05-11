using System;
using System.Collections.Generic;
using System.Text;

namespace AppLoginFlow.Model
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
