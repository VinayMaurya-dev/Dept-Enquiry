using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CourtCase.DAL;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public class Utility
{




}
public static class Encrypt
{
    public static string encrypt(string ToEncrypt)
    {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
    }
    public static string decrypt(string cypherString)
    {
        return Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
    }
    public static string GetIp()
    {
        string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }

    public static string EncryptString(string plainText, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyBytes;
            aesAlg.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    csEncrypt.FlushFinalBlock();
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }
    public static string DecryptString(string cipherText, string key)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.IV = cipherBytes.Take(aesAlg.IV.Length).ToArray();

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes.Skip(aesAlg.IV.Length).ToArray()))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

}


