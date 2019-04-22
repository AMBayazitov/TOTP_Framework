using System;
using System.Text;
using TOTP_Framework.Interfaces;

namespace TOTP_Framework
{
    public class OtpGenerator
    {
        private readonly string _key;

        private readonly int _passwordDuration;

        private readonly int _timeToSync;

        private readonly int _numberOfCharInTheCode;

        private readonly IAlgorithm _algorithm;

        public OtpGenerator(string key, IAlgorithm algorithm)
        {
            _key = key;
            _passwordDuration = 30;
            _timeToSync = 10;
            _numberOfCharInTheCode = 6;
            _algorithm = algorithm;
        }

        public OtpGenerator(string key, int passwordDuration, IAlgorithm algorithm)
        {
            _key = key;
            _passwordDuration = passwordDuration;
            _timeToSync = 10;
            _numberOfCharInTheCode = 6;
            _algorithm = algorithm;
        }

        public OtpGenerator(string key, int passwordDuration, int timeToSync, IAlgorithm algorithm)
        {
            _key = key;
            _passwordDuration = passwordDuration;
            _timeToSync = timeToSync;
            _algorithm = algorithm;
        }

        public OtpGenerator(string key, int passwordDuration, int timeToSync, int numberOfCharInTheCode, IAlgorithm algorithm)
        {
            _key = key;
            _passwordDuration = passwordDuration;
            _timeToSync = timeToSync;
            _numberOfCharInTheCode = numberOfCharInTheCode;
            _algorithm = algorithm;
        }

        // Генерация OTP
        public string GenerateCode()
        {
            var timeCounter = ((CurrentUnixTime() - _timeToSync) / _passwordDuration).ToString();
            var hash = _algorithm.Generate(timeCounter, _key);

            var code = new StringBuilder();
            var hashArr = ToNumbString(hash);
            var lastByte = LastByte(hash);

            for (var i = 0; i < _numberOfCharInTheCode; i++)
            {
                code.Append(hashArr[lastByte + i]);
            }

            return code.ToString();
        }

        /// <summary>
        /// Перевод символов из hex в string(dec) 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static string ToNumbString(string hexString)
        {
            var numbString = BitConverter.ToString(Encoding.ASCII.GetBytes(hexString)).Replace("-", string.Empty);
            return numbString;
        }



        private static long CurrentUnixTime() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();


        // Байт, с которого начинать отсчет
        private int LastByte(string hash)
        {
            var lastByte = hash[hash.Length - 1].ToString();
            return int.Parse(lastByte, System.Globalization.NumberStyles.HexNumber);
        }

    }
}
