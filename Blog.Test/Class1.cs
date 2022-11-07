using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Test
{
    public class Validate
    {
        MyContext context = new MyContext();
        public bool AdminTest()
        {
            bool ans = false;
            var result = context.AdminInfos.ToList();

            if (result[0].EmailId == "admin@gmail.com" && result[0].Password == "Admin@123")
            {
                ans = true;
            }
            return ans;

        }
    }
}
