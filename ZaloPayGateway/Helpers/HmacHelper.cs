using System.Security.Cryptography;

namespace ZaloPayGateway.Helpers
{
    public enum ZaloPayHMAC
    {
        HMACMD5,
        HMACSHA1,
        HMACSHA256,
        HMACSHA512
    }

    public class HmacHelper
    {
        public static string Compute(string key = "", string message = "",ZaloPayHMAC algorithm = ZaloPayHMAC.HMACSHA256)
        {
            byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] hashMessage = algorithm switch
            {
                ZaloPayHMAC.HMACMD5 => new HMACMD5(keyByte).ComputeHash(messageBytes),
                ZaloPayHMAC.HMACSHA1 => new HMACSHA1(keyByte).ComputeHash(messageBytes),
                ZaloPayHMAC.HMACSHA256 => new HMACSHA256(keyByte).ComputeHash(messageBytes),
                ZaloPayHMAC.HMACSHA512 => new HMACSHA512(keyByte).ComputeHash(messageBytes),
                _ => new HMACSHA256(keyByte).ComputeHash(messageBytes)
            };

            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }
}