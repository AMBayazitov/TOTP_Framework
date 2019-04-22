using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TOTP_Framework.Interfaces;

namespace TOTP_Framework.Algorithms
{
    public sealed class HmacSha256 : IAlgorithm
    {
        public string Generate(string timeCounter, string key)
        {
            var sha = new HMACSHA256(Encoding.ASCII.GetBytes(timeCounter));
            var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(key));
            return sha.ComputeHash(memoryStream).Aggregate("", (s, e) => s + $"{e:x2}", s => s);
        }
    }
}
