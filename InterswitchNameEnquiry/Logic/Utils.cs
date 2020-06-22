using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using NLog;

namespace InterswitchNameEnquiry.Logic
{
    public class Utils
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void LogError(Exception e, string message)
        {
            logger.Error(e, message);
        }

        public static void LogInfo(string message)
        {
            logger.Info(message);
        }
        public static string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789._";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetToken()
        {
            return Base64StringEncoder(encrypt(RandomString(50) + "|" + DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss")));
        }
        public static bool IsAccessTokenValid(string access_token)
        {
            var isValid = false;
            try
            {
                if (!string.IsNullOrEmpty(access_token))
                {
                    logger.Info("from request : " + access_token);
                    var rawAccessToken = Base64StringDecoder(Decrypt(access_token));
                    logger.Info("real  : " + rawAccessToken);
                    if (rawAccessToken.Contains("|"))
                    {
                        var dateTimeComponentStr = rawAccessToken.Split('|')[1];
                        var dateTimeComponent = DateTime.ParseExact(dateTimeComponentStr, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        if (dateTimeComponent.AddHours(1) < DateTime.Now)
                            isValid = true;
                    }

                }
            }
            catch (Exception e) { logger.Error(e); }
            return isValid;
        }
        public static string OracleConnection
        {
            get { return ConfigurationManager.AppSettings["OracleConnection"]; }

        }
        public static bool UseDummyDataForOracleRequest
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["UseDummyDataForOracleRequest"]); }

        }
        public static string InterswitchRequestClientId
        {
            get
            {
                try
                {
                    var str = ConfigurationManager.AppSettings["InterswitchRequestClientId"];
                    if (!string.IsNullOrEmpty(str)) { return str; } else { return "InterswitchRequestClientId not set"; }
                }
                catch { return "InterswitchRequestClientId not valid"; }
            }

        }
        public static string InterswitchRequestSecretKey
        {
            get
            {
                try
                {
                    var str = ConfigurationManager.AppSettings["InterswitchRequestSecretKey"];
                    if (!string.IsNullOrEmpty(str)) { return str; } else { return "InterswitchRequestSecretKey not set"; }
                }
                catch { return "InterswitchRequestSecretKey not valid"; }
            }

        }

        public static string Base64StringDecoder(string encodedString)
        {
            var decodedStr = string.Empty;
            try
            {
                byte[] data = Convert.FromBase64String(encodedString);
                decodedStr = Encoding.UTF8.GetString(data);
            }
            catch (Exception e) { logger.Error(e); }
            return decodedStr;
        }
        public static string Base64StringEncoder(string str)
        {
            var encodedStr = string.Empty;
            if (string.IsNullOrEmpty(str))
                return str;
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
                encodedStr = Convert.ToBase64String(data);
            }
            catch (Exception e) { logger.Error(e); }
            return encodedStr;
        }
        public static string Sha512Hash(string str, bool lower = false)
        {
            var hash = string.Empty;
            var data = Encoding.UTF8.GetBytes(str);
            using (SHA512 shaM = new SHA512Managed())
            {
                var hashByte = shaM.ComputeHash(data);
                hash = BitConverter.ToString(hashByte).Replace("-", "");
            }
            if (lower)
                hash = hash.ToLower();
            return hash;
        }
    }
}