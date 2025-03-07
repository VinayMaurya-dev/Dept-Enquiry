using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CourtCase.Filters
{
	public static class UrlEncoding
	{
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string iconclass)
        {


            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + "'" + d.Values.ElementAt(i) + "'";
                }
            }
             
            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }
            ancor.Append(" href='");
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
            }

            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                ancor.Append("?q=" + EncryptURL(queryString));
            }
            ancor.Append("'");
            ancor.Append(">");
            if (!string.IsNullOrEmpty(iconclass))
                ancor.Append("<i class='" + iconclass + "'></i> ");
            ancor.Append(linkText);
            ancor.Append("</a>");
            return new MvcHtmlString(ancor.ToString());
        }
        public static MvcHtmlString EncodedActionLinkWithTwoParams(
                    this HtmlHelper htmlHelper,
                    string linkText,
                    string actionName,
                    string controllerName,
                    object routeValues,
                    object htmlAttributes,
                    string iconClass,
                    string opValue
                    )
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;

            if (routeValues != null)
            {
                RouteValueDictionary routeValueDict = new RouteValueDictionary(routeValues);
                foreach (var key in routeValueDict.Keys)
                {
                    queryString += key + "=" + routeValueDict[key] + "&";
                }

                // Remove the trailing '&' if there are any route values
                if (!string.IsNullOrEmpty(queryString))
                {
                    queryString = queryString.TrimEnd('&');
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary htmlAttributesDict = new RouteValueDictionary(htmlAttributes);
                foreach (var key in htmlAttributesDict.Keys)
                {
                    htmlAttributesString += " " + key + "=\'" + htmlAttributesDict[key] + "\'";
                }
            }

            StringBuilder anchor = new StringBuilder();
            anchor.Append("<a");

            if (!string.IsNullOrEmpty(htmlAttributesString))
            {
                anchor.Append(htmlAttributesString);
            }

            anchor.Append(" href='");

            if (!string.IsNullOrEmpty(controllerName))
            {
                anchor.Append("/" + controllerName);
            }

            if (!string.Equals(actionName, "Index", StringComparison.OrdinalIgnoreCase))
            {
                anchor.Append("/" + actionName);
            }

            if (!string.IsNullOrEmpty(queryString) || !string.IsNullOrEmpty(opValue))
            {
                anchor.Append("?");

                if (!string.IsNullOrEmpty(queryString))
                {
                    anchor.Append("q=" + EncryptURL(queryString));
                }

                if (!string.IsNullOrEmpty(opValue))
                {
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        anchor.Append("&");
                    }

                    anchor.Append("op=" + opValue);
                }
            }

            anchor.Append("'>");

            if (!string.IsNullOrEmpty(iconClass))
            {
                anchor.Append("<i class='" + iconClass + "'></i> ");
            }

            anchor.Append(linkText);
            anchor.Append("</a>");

            return new MvcHtmlString(anchor.ToString());
        }
        public static MvcHtmlString EncodedActionLinkWithFiveParams(this HtmlHelper htmlHelper,string linkText,string actionName,string controllerName,object routeValues,object htmlAttributes,string iconClass,string opValue,string RelatedTo,string ZoneId,string CircleId,string DivisionId)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;

            if (routeValues != null)
            {
                RouteValueDictionary routeValueDict = new RouteValueDictionary(routeValues);
                foreach (var key in routeValueDict.Keys)
                {
                    queryString += key + "=" + routeValueDict[key] + "&";
                }

                // Remove the trailing '&' if there are any route values
                if (!string.IsNullOrEmpty(queryString))
                {
                    queryString = queryString.TrimEnd('&');
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary htmlAttributesDict = new RouteValueDictionary(htmlAttributes);
                foreach (var key in htmlAttributesDict.Keys)
                {
                    htmlAttributesString += " " + key + "='" + htmlAttributesDict[key] + "'";
                }
            }

            StringBuilder anchor = new StringBuilder();
            anchor.Append("<a");

            if (!string.IsNullOrEmpty(htmlAttributesString))
            {
                anchor.Append(htmlAttributesString);
            }

            anchor.Append(" href='");

            if (!string.IsNullOrEmpty(controllerName))
            {
                anchor.Append("/" + controllerName);
            }

            if (!string.Equals(actionName, "Index", StringComparison.OrdinalIgnoreCase))
            {
                anchor.Append("/" + actionName);
            }

            if (!string.IsNullOrEmpty(queryString) || !string.IsNullOrEmpty(opValue) ||
                !string.IsNullOrEmpty(RelatedTo) ||
                !string.IsNullOrEmpty(ZoneId) ||
                !string.IsNullOrEmpty(CircleId) ||
                !string.IsNullOrEmpty(DivisionId))
            {
                anchor.Append("?");

                if (!string.IsNullOrEmpty(queryString))
                {
                    anchor.Append("q=" + EncryptURL(queryString));
                }

                if (!string.IsNullOrEmpty(opValue))
                {
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        anchor.Append("&");
                    }
                    anchor.Append("op=" + opValue);
                }

                if (!string.IsNullOrEmpty(RelatedTo))
                {
                    if (!string.IsNullOrEmpty(queryString) || !string.IsNullOrEmpty(opValue))
                    {
                        anchor.Append("&");
                    }
                    anchor.Append("InvestigatingOfficerRelatedTo=" + RelatedTo);
                }

                if (!string.IsNullOrEmpty(ZoneId))
                {
                    anchor.Append("&InvestigatingOfficerZoneId=" + ZoneId);
                }

                if (!string.IsNullOrEmpty(CircleId))
                {
                    anchor.Append("&InvestigatingOfficerCircleId=" + CircleId);
                }

                if (!string.IsNullOrEmpty(DivisionId))
                {
                    anchor.Append("&InvestigatingOfficerDivisionId=" + DivisionId);
                }
            }

            anchor.Append("'>");

            if (!string.IsNullOrEmpty(iconClass))
            {
                anchor.Append("<i class='" + iconClass + "'></i> ");
            }

            anchor.Append(linkText);
            anchor.Append("</a>");

            return new MvcHtmlString(anchor.ToString());
        }

        public static string EncryptURL(string clearText)
        { 
            string EncryptionKey = "VINAYLLUPHLAKMAKJ2SPBNI992128005462401";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
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