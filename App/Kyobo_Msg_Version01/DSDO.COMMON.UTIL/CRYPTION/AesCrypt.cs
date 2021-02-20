using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.CRYPTION
{
    public class AesCrypt
    {
        /// <summary>
        /// 암호 데이터 문자열을 주어진 암호로 암호화 / 복호화
        /// </summary>
        /// <param name="cryptData">string data to encrypt</param>
        /// <param name="cryptPwd">password string</param>
        /// <param name="cryptType">crypt type</param>
        /// <returns>encrypted/decrypted data</returns>
        public static string GetCrypt(string cryptData, string cryptPwd, CryptType cryptType)
        {
            return GetCrypt(cryptData, cryptPwd, null, cryptType);
        }

        /// <summary>
        /// 암호 데이터 문자열을 주어진 암호로 암호화 / 복호화
        /// </summary>
        /// <param name="cryptData">string data to encrypt</param>
        /// <param name="cryptPwd">password string</param>
        /// <param name="cryptType">crypt type</param>
        /// <returns>encrypted/decrypted data</returns>
        /// <remarks>if keySalt is null, then default keySalt is used</remarks>
        public static string GetCrypt(string cryptData, string cryptPwd, byte[] keySalt, CryptType cryptType)
        {
            try
            {
                byte[] cryptBytes = null;
                switch (cryptType)
                {
                    case CryptType.Encrypt:
                        cryptBytes = Encoding.Unicode.GetBytes(cryptData);
                        break;
                    case CryptType.Decrypt:
                        cryptBytes = Convert.FromBase64String(cryptData);
                        break;
                }

                byte[] retData = GetCrypt(cryptBytes, cryptPwd, keySalt, cryptType);
                switch (cryptType)
                {
                    case CryptType.Encrypt:
                        return Convert.ToBase64String(retData);
                    case CryptType.Decrypt:
                        return Encoding.Unicode.GetString(retData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 암호 데이터 문자열을 주어진 암호로 암호화 / 복호화
        /// </summary>
        /// <param name="cryptData">data to crypt</param>
        /// <param name="cryptPwd">password string</param>
        /// <param name="cryptType">crypt type</param>
        /// <returns>encrypted/decrypted data</returns>
        public static byte[] GetCrypt(byte[] cryptData, string cryptPwd, CryptType cryptType)
        {
            return GetCrypt(cryptData, cryptPwd, null, cryptType);
        }

        /// <summary>
        /// Encrypt/Decypt the given cryptData with the given password
        /// </summary>
        /// <param name="cryptData">data to crypt</param>
        /// <param name="offset">offset of cryptData for crypt</param>
        /// <param name="count">length for crypt</param>
        /// <param name="cryptPwd">password string</param>
        /// <param name="cryptType">crypt type</param>
        /// <returns>encrypted/decrypted data</returns>
        public static byte[] GetCrypt(byte[] cryptData, int offset, int count, string cryptPwd, CryptType cryptType)
        {
            return GetCrypt(cryptData, offset, count, cryptPwd, null, cryptType);
        }

        /// <summary>
        /// 암호 데이터 문자열을 주어진 암호로 암호화 / 복호화
        /// </summary>
        /// <param name="cryptData">data to crypt</param>
        /// <param name="cryptPwd">password string</param>
        /// <param name="keySalt">salt string</param>
        /// <param name="cryptType">crypt type</param>
        /// <returns>encrypted/decrypted data</returns>
        /// <remarks>if keySalt is null, then default keySalt is used</remarks>
        public static byte[] GetCrypt(byte[] cryptData, string cryptPwd, byte[] keySalt, CryptType cryptType)
        {
            return GetCrypt(cryptData, 0, cryptData.Length, cryptPwd, keySalt, cryptType);
        }


        /// <summary>
        /// 암호 데이터 문자열을 주어진 암호로 암호화 / 복호화
        /// </summary>
        /// <param name="cryptData">data to crypt</param>
        /// <param name="offset">offset of cryptData for crypt</param>
        /// <param name="count">length for crypt</param>
        /// <param name="cryptPwd">password string</param>
        /// <param name="keySalt">salt string</param>
        /// <param name="cryptType">crypt type</param>
        /// <returns>encrypted/decrypted data</returns>
        /// <remarks>if keySalt is null, then default keySalt is used</remarks>
        public static byte[] GetCrypt(byte[] cryptData, int offset, int count, string cryptPwd, byte[] keySalt, CryptType cryptType)
        {
            if (keySalt == null)
                keySalt = new byte[] { 0x54, 0x81, 0x45, 0x4A, 0x3B, 0x5E, 0x52, 0x15, 0x86, 0x5A, 0x40, 0x3B, 0xB4 };
            //PasswordDeriveBytes pwdBytes = new PasswordDeriveBytes(cryptPwd, keySalt);
            Rfc2898DeriveBytes pwdBytes = new Rfc2898DeriveBytes(cryptPwd, keySalt);

            AesManaged crypAlg = new AesManaged();
            crypAlg.Key = pwdBytes.GetBytes(32);
            crypAlg.IV = pwdBytes.GetBytes(16);

            try
            {
                ICryptoTransform cryptoTranform = (cryptType == CryptType.Encrypt) ? crypAlg.CreateEncryptor() : crypAlg.CreateDecryptor();

                MemoryStream memStream = new MemoryStream();
                using (CryptoStream cryptStream = new CryptoStream(memStream, cryptoTranform, CryptoStreamMode.Write))
                {
                    cryptStream.Write(cryptData, offset, count);
                    cryptStream.FlushFinalBlock();
                    return memStream.ToArray();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
            }
            return null;
        }


    }
}
