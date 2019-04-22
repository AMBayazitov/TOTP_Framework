using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOTP_Framework.Algorithms;

namespace TOTP_Framework
{
    class Example
    {
        static void Main(string[] args)
        {
            var generator = new OtpGenerator("teststring", new HmacSha256());
            Console.WriteLine(generator.GenerateCode());
            Console.ReadLine();
        }
    }
}
