using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hello.Common.Helper
{
    public class AppHelper
    {
        public static bool isEmail(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            try
            {
                return Regex.IsMatch(str,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static DateTime StringToDate(string str)
        {
            return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static double StringToNumber(string str)
        {
            double num = 0;
            double.TryParse(str, out num);

            return num;
        }

        public static string RemoveUnicode(string inputText)
        {
            string strFormD = inputText.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            string str = "";
            for (int i = 0; i <= strFormD.Length - 1; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(strFormD[i]);
                if (uc == UnicodeCategory.NonSpacingMark == false)
                {
                    if (strFormD[i] == 'đ')
                        str = "d";
                    else if (strFormD[i] == 'Đ')
                        str = "D";
                    else
                        str = strFormD[i].ToString();

                    sb.Append(str);
                }
            }

            return sb.ToString().ToLower();
        }
    }
}
