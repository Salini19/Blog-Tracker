using DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Test
{
    [TestFixture]
    public class Tests
    {

        [TestCase]
        public void Employee_mail_Test()
        {

            MyContext context = new MyContext();
            var found = context.AdminInfos.ToList();

            Assert.AreEqual("admin@gmail.com", found[0].EmailId);
            Console.WriteLine("Validation success");

        }


        [TestCase]
        public void Admin_password_Test()
        {
            MyContext context = new MyContext();
            var found = context.AdminInfos.ToList();


            Assert.AreEqual("Admin@123", found[0].Password);
            Console.WriteLine("Validation success");

        }
        Validate t = new Validate();
        [TestCase]
        public void AdminTest()
        {
            t.AdminTest();


        }

    }
}
