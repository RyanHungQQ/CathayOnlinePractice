using Common.Common;
using Common.Encryptions;

namespace Common.Extensions
{
    public static class AESExtension
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decrypt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            try
            {
                value = AESHelper.Decrypt(value, Setting.AESKey, Setting.AESIV);
            }
            catch { }
            return value;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            try
            {
                value = AESHelper.Encrypt(value, Setting.AESKey, Setting.AESIV);
            }
            catch { }
            return value;
        }
    }
}
