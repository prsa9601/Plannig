using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application
{
    public class GenerateCode
    {
        public string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateSecureRandomCode(int length)
        {
            const string chars = "aqwszxdecrfvbgtyhnjumk?!@#+_-iol./p'[]ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var data = new byte[length];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            return new string(data.Select(b => chars[b % chars.Length]).ToArray());
        }
        public string GenerateTimestampedCode()
        {
            return $"{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid()}";
        }
        public string GenerateGuidCode()
        {
            return Guid.NewGuid().ToString();
        }

    }
}

