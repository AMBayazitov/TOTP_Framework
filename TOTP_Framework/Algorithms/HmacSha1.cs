using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TOTP_Framework.Interfaces;

namespace TOTP_Framework.Algorithms
{
    public sealed class HmacSha1 : IAlgorithm
    {
        public string Generate(string timeCounter, string key)
        {
            var sha = new HMACSHA1(Encoding.ASCII.GetBytes(key));
            var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(timeCounter));
            return sha.ComputeHash(memoryStream).Aggregate("", (s, e) => s + $"{e:x2}", s => s);
        }
    }
}
