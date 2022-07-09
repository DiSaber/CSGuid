using System.Security.Cryptography;
using System.Text;

namespace CSGuid
{
    /// <summary>
    ///  Creates a cryptographically secure Guid using a specified number of random bytes (entropy)
    /// </summary>
    public class CryptoGuid
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        public byte[] GuidBytes { get; }
        public string GuidString { get; }

        public CryptoGuid(int bytes = 512)
        {
            GuidBytes = RandomBytes(bytes);
            GuidString = Sha512(GuidBytes);
        }

        private static string Sha512(byte[] input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(input);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static byte[] RandomBytes(int bytes)
        {
            byte[] guid = new byte[bytes];
            rng.GetBytes(guid);
            return guid;
        }
    }
}
