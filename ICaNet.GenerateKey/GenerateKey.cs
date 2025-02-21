using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.GenerateKey
{
    public class GenerateKey
    {
        public string GenerateStrongKey(int keySize = 32)
        {
            byte[] key = new byte[keySize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return BitConverter.ToString(key).Replace("-", "");
        }
    }
}
