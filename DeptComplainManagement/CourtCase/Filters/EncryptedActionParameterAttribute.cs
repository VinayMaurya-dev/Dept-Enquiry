using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Util;

namespace CourtCase.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var decryptedParameters = new Dictionary<string, object>();

            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                string decryptedString = Decrypt(encryptedQueryString);
                string[] paramsArrs = decryptedString.Split('&'); // Use '&' for splitting key-value pairs

                foreach (var param in paramsArrs)
                {
                    string[] paramArr = param.Split('=');
                    if (paramArr.Length == 2)
                    {
                        string key = paramArr[0];
                        string value = paramArr[1];

                        if (int.TryParse(value, out int intValue))
                        {
                            decryptedParameters[key] = intValue;
                        }
                        else
                        {
                            decryptedParameters[key] = value;
                        }
                    }
                }
            }

            foreach (var param in decryptedParameters)
            {
                if (filterContext.ActionParameters.ContainsKey(param.Key))
                {
                    filterContext.ActionParameters[param.Key] = param.Value;
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "VINAYLLUPHLAKMAKJ2SPBNI992128005462401";

            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
    }
     
}