using System;
using System.Text.RegularExpressions;

namespace MI.CloudPlatform.Util
{
    public class Validate
    {
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Boolean CheckCellPhone(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return false;
            }
            else
            {
                Regex regex = new Regex(@"^((\(\d{2,3}\))|(\d{3}\-))?((^1[34578]\d{9}))$");
                if (regex.IsMatch(text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="str"></param>
        /// <returns>0:表示匹配</returns>
        public static Boolean CheckIsID(string str)
        {
            string str1 = "0", str2 = "0";
            str1 = str.Remove(str.Length - 1, 1);
            Regex reg1
               = new Regex(@"^[-]?\d+[.]?\d*$");
            if (reg1.IsMatch(str1) == true)
            {
                if (str.Length == 15)
                {
                    str2 = str1.Remove(0, 6);
                    str2 = str2.Remove(6, 2);
                    str2 = "19" + str2;
                    str2 = str2.Insert(4, "-");
                    str2 = str2.Insert(7, "-");
                    Regex reg2 = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                    if (reg2.IsMatch(str2) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (str.Length == 18)
                    {
                        str2 = str1.Remove(0, 6);
                        str2 = str2.Remove(8, 3);
                        str2 = str2.Insert(4, "-");
                        str2 = str2.Insert(7, "-");
                        Regex reg2 = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                        if (reg2.IsMatch(str2) == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证非中文
        /// </summary>
        /// <param name="text"></param>
        /// <returns>0:表示匹配</returns>
        public static Boolean CheckWord(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return false;
            }
            else
            {
                Regex regex = new Regex("^[a-zA-Z0-9]+$");
                if (regex.IsMatch(text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 验证EMAIL
        /// </summary>
        /// <param name="text"></param>
        /// <returns>0:表示匹配</returns>
        public static Boolean CheckMail(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return false;
            }
            else
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+\.(?:com|cn)$");
                if (regex.IsMatch(text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
