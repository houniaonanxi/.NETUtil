using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace MI.CloudPlatform.Util
{
    public class Tools
    {
        #region 替换指定的字符串
        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }
        #endregion

        #region 生成随机字母与数字
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <param name="IsPureNum">是否全是数字</param>
        /// <returns></returns>
        public static string CreateRandomCode(int length, bool sleep, bool IsPureNum)
        {
            if (sleep)
            {
                Thread.Sleep(1);
            }
            char[] arrayRandom = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8','9',
                                              'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H','I', 'J', 
                                              'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S','T', 
                                               'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                                               'u', 'v', 'w', 'x', 'y', 'z',
                                               'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                                               'U', 'V', 'W', 'X', 'Y', 'Z'};
            string result = string.Empty;
            int t = IsPureNum ? 10 : arrayRandom.Length;

            Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                result += arrayRandom[random.Next(0, t)];
            }
            return result;
        }
        #endregion

        #region 检测是否有Sql危险字符

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        #endregion

        #region 生成指定长度的字符串

        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }
        #endregion

        #region 获取随机rgb颜色值
        /// <summary>
        /// 生成高亮颜色
        /// </summary>
        /// <returns>高亮颜色字符串(exp:#ffffff)</returns>
        public static string GetHighRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            // 对于C#的随机数
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            // 为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            string color = Color.FromArgb(int_Red, int_Green, int_Blue).Name.Substring(2, 6);
            return "#" + (color == "ffffff" ? GetHighRandomColor() : color);
        }

        /// <summary>
        /// 生成暗黑颜色
        /// </summary>
        /// <returns>暗黑颜色字符串(exp:#000000)</returns>
        public static string GetLowRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            // 对于C#的随机数
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            // 为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(160);
            int int_Green = RandomNum_Sencond.Next(160);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            string color = Color.FromArgb(int_Red, int_Green, int_Blue).Name.Substring(2, 6);
            return "#" + (color == "ffffff" ? GetLowRandomColor() : color);
        }
        #endregion

        #region 获取当前网站域名

        /// <summary>
        /// 获取当前网站域名
        /// </summary>
        public static string GetWebURL()
        {
            var url = HttpContext.Current.Request.Url;
            return url.ToString().Replace(url.AbsolutePath, "");
        }
        #endregion

        #region 清空HTML标记
        /// <summary>
        /// 清空HTML标记
        /// </summary>
        /// <param name="strHtml">字符串</param>
        public static string ClearHtmlTags(string html, int length = 0)
        {
            if (!string.IsNullOrEmpty(html))
            {
                string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
                strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

                if (length > 0 && strText.Length > length)
                {
                    return strText.Substring(0, length);
                }

                strText = strText.Replace("\r", "");
                strText = strText.Replace("\n", "");
                strText = strText.Replace("'", "‘");
                strText = strText.Replace(" ", "").Trim();
                return strText;
            }
            return "";
        }
        #endregion

        #region 获取字符串首字母
        /// <summary>
        /// 获取字符串的首字母
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>姓名的首字母</returns>
        public static string Get_String_FristEn(string str)
        {
            String _Temp = null;
            for (int i = 0; i < str.Length; i++)
                _Temp = _Temp + GetOneIndex(str.Substring(i, 1));
            return _Temp.Substring(0, 1).ToUpper();
        }

        //得到单个字符的首字母
        private static String GetOneIndex(String OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {

                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(
             String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
             + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
             ));
            }

        }

        //根据区位得到首字母
        private static String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = "cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr"
                 + "ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss"
                 + "pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd"
                 + "nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma"
                 + "eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt"
                 + "rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx"
                 + "jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj"
                 + "flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj"
                 + "mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql"
                 + "cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc"
                 + "xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc"
                 + "xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj"
                 + "hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom"
                 + "yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd"
                 + "chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb"
                 + "nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs"
                 + "xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl"
                 + "jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh"
                 + "gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy"
                 + "hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql"
                 + "dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy"
                 + "whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd"
                 + "ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj"
                 + "gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf"
                 + "jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg"
                 + "yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd"
                 + "xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg"
                 + "dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm"
                 + "ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx"
                 + "swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj"
                 + "lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj"
                 + "btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd"
                 + "jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return " ";
        }
        #endregion

        #region 生成ID

        /// <summary>
        /// 获取GUID
        /// </summary>
        /// <returns></returns>
        public static string GenTableID()
        {
            return Guid.NewGuid().ToString("N");
        }
        #endregion

        #region 获取当前用户SessionID
        /// <summary>
        /// 获取GetSessionID
        /// </summary>
        public static string GetSessionID()
        {
            return HttpContext.Current.Session.SessionID;
        }
        #endregion

        #region 获取当前访问客户端IP
        /// <summary>
        /// 获取访问者IP
        /// </summary>
        /// <returns>访问者IP</returns>
        public static string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
        #endregion

        #region 获取服务器时间
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns>服务器时间</returns>
        public static DateTime GetServerTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获取服务器日期
        /// </summary>
        /// <returns>服务器日期</returns>
        public static DateTime GetServerDate()
        {
            return DateTime.Now.Date;
        }
        #endregion

        #region 生成随机20位单位编码
        /// <summary>
        /// 生成随机20位单位编码
        /// </summary>
        /// <returns></returns>
        public static string GenUnitCode()
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            Random randrom = new Random((int)DateTime.Now.Ticks);

            string str = "";
            for (int i = 0; i < 20; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }
            return str;
        }
        #endregion

        #region C# DateTime时间格式转换为Unix时间戳格式
        /// <summary>  
        /// 将C# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long GetUnix(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
        #endregion

        #region 时间戳转为C#格式时间
        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime UnixToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion

        #region 获取汉字首字母
        /// <summary>
        /// 汉字转化为拼音首字母
        /// </summary>
        /// <param name="str">汉字</param>
        /// <returns>首字母</returns>
        public static string GetFirstPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }
        #endregion

        #region 汉字转拼音
        /// <summary>
        /// 汉字转化为拼音
        /// </summary>
        /// <param name="str">汉字</param>
        /// <returns>全拼</returns>
        public static string GetPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }
        #endregion

        #region 读取文件内容

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        public static string ReaderFile(string path)
        {
            string fileData = string.Empty;

            try
            {   ///读取文件的内容      
                StreamReader reader = new StreamReader(GetMapPath(path), Encoding.Default);
                fileData = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return fileData;
        }

        #endregion

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        #region 获取客户端IP
        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        /// <summary>
        /// 拼接任务html
        /// </summary>
        /// <param name="title"></param>
        /// <param name="percent"></param>
        /// <param name="hasinputcount"></param>
        /// <param name="hasnoinputcount"></param>
        /// <returns></returns>
        public static string ConcatTaskHtml(string title, string percent, int hasinputcount, int hasnoinputcount)
        {
            var content = "<div class=\"col-lg-6\">";
            content += "<section class=\"panel\"><div class=\"panel-heading\"><h4>" + "【" + title + "】</h4> </div>";
            content += "<div style=\"padding-bottom:15px;\"> <div class=\"weather-category twt-category\" style=\"padding-bottom:0;\">";
            content += "<div style=\"width:80%;text-align:left;border-right:none !important;font-size:14px;padding-left:10px;line-height:25px;\">";
            content += "<div class=\"col-sm-12\"><div class=\"col-sm-10\" style=\"padding-left:0;padding-right:0\"><div style=\"margin-top:5px:margin-bottom:5px\" class=\"progresstask progress-striped\">";
            content += "<div style=\"width:" + percent + ";\" aria-valuemax=\"100\" aria-valuemin=\"0\" aria-valuenow=\"" + percent + "\" role=\"progressbar\" class=\"progress-bar progress-bar-success\"></div> ";
            content += "</div></div><div class=\"col-sm-2\" style=\"padding-left:0;padding-right:0\"><span style=\"font-size:13px;color:#000000\">" + percent + "</span></div></div></div><div style=\"width:100%;text-align:left;border-right:none !important;font-size:14px;padding-left:10px;line-height:25px;\">";
            content += "<div class=\"col-sm-12\">已录成绩  " + hasinputcount + "</div></div><div style=\"width:100%;text-align:left;border-right:none !important;font-size:14px;padding-left:10px;line-height:25px;\">";
            content += "<div class=\"col-sm-12\">未录成绩  " + hasnoinputcount + "</div></div> </div>  </div></section></div> ";
            return content;
        }
        /// <summary>
        /// 小数点取两位0.8976  0.89
        /// </summary>
        /// <param name="finishcount"></param>
        /// <param name="allcount"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static string GetPercet(int finishcount, int allcount, out string percent)
        {
            string per = (finishcount / double.Parse(allcount.ToString()) * 100).ToString();
            int perlen = per.LastIndexOf(".");
            if (per.Length > 4) { percent = per.Substring(0, perlen + 3) + "%"; }
            else
            {
                percent = per + "%";
            }
            return percent;
        }
        /// <summary>
        /// 获取距离当前时间
        /// </summary>
        /// <param name="dt">目标时间</param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }
        /// <summary>
        /// 异步分页
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总数据量</param>
        /// <param name="unit_cn">显示单位名称</param>
        /// <param name="jsFunction">前端js调用函数名称</param>
        /// <param name="pageSize">页显示数量</param>
        /// <param name="type">查询类型（历史分页专用）</param>
        /// <param name="paperWidth">分页长度</param>
        /// <returns></returns>
        public static string CreatePager(int pageIndex, int totalCount, string unit_cn, string jsFunction, int pageSize = 10, int type = 0, int paperWidth = 9)
        {
            //<div class="row"><div class="col-sm-6"><div class="dataTables_info" id="sample_1_info">Showing 1 to 10 of 25 entries</div></div><div class="col-sm-6"><div class="dataTables_paginate paging_bootstrap pagination"><ul><li class="prev disabled"><a href="#">← Prev</a></li><li class="active"><a href="#">1</a></li><li><a href="#">2</a></li><li><a href="#">3</a></li><li class="next"><a href="#">Next → </a></li></ul></div></div></div>


            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class=\"col-sm-" + (12 - paperWidth).ToString() + "\"><div class=\"dataTables_info\" id=\"sample_1_info\">共 {0} 条</div></div>", totalCount));

            sb.Append("<div class=\"col-sm-" + paperWidth + "\"><div class=\"dataTables_paginate paging_bootstrap pagination\"><ul>");

            int pageCount = (int)Math.Ceiling((double)totalCount / (double)pageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;
            if (end > pageCount)
            {
                end = pageCount;
                start = end - 9 > 0 ? end - 9 : 1;
            }
            if (pageIndex > 1)
            {
                sb.Append(string.Format("<li class=\"prev disabled\"><a onclick='{0}({1}{2})'>上页</a></li>", jsFunction, pageIndex - 1, type == 0 ? "" : "," + type.ToString()));
            }
            for (var i = start; i <= end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(string.Format("<li class='active'><a>{0}</a></li>", i));
                }
                else
                {
                    sb.Append(string.Format("<li><a onclick='{0}({1}{2})'>{1}</a></li>", jsFunction, i, type == 0 ? "" : "," + type.ToString()));
                }
            }
            if (pageIndex < pageCount)
            {
                sb.Append(string.Format("<li class=\"next\"><a onclick='{0}({1}{2})'>下页</a></li>", jsFunction, pageIndex + 1, type == 0 ? "" : "," + type.ToString()));
            }
            sb.Append("</ul></div>");
            return sb.ToString();
        }

        /// <summary>
        /// 同步分页
        /// </summary>
        /// <param name="path">跳转路径</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageSize">页显示量</param>
        /// <param name="param">自定义参数</param>
        /// <returns></returns>
        public static string CreatePager(string path, int pageIndex, int totalCount, int pageSize = 10, string param = "")
        {
            int pageCount = (int)Math.Ceiling((double)totalCount / (double)pageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='pagination'>");
            int start = pageIndex - 5;
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;
            if (end > pageCount)
            {
                end = pageCount;
                start = end - 9 > 0 ? end - 9 : 1;
            }
            if (pageIndex > 1)
            {
                sb.Append(string.Format("<li><a href='{0}?index=1{1}'>首页</a></li>", path, param));
                sb.Append(string.Format("<li><a href='{0}?index={1}{2}'>上页</a></li>", path, pageIndex - 1, param));
            }
            else
            {
                sb.Append("<li class='disabled'><a>首页</a></li>");
                sb.Append("<li class='disabled'><a>上页</a></li>");
            }
            for (var i = start; i <= end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(string.Format("<li class='active'><a>{0}</a></li>", i));
                }
                else
                {
                    sb.Append(string.Format("<li><a href='{0}?index={1}{2}'>{1}</a></li>", path, i, param));
                }
            }
            if (pageIndex < pageCount)
            {
                sb.Append(string.Format("<li><a href='{0}?index={1}{2}'>下页</a></li>", path, pageIndex + 1, param));
                sb.Append(string.Format("<li><a href='{0}?index={1}{2}'>尾页</a></li>", path, pageCount, param));
            }
            else
            {
                sb.Append("<li class='disabled'><a>下页</a></li>");
                sb.Append("<li class='disabled'><a>尾页</a></li>");
            }
            sb.Append("</ul>");
            return sb.ToString();
        }

        public static string Base64StringToImage(string dirpath, string imageName, string base64Str)
        {
            try
            {
                if (!Directory.Exists(dirpath))
                    Directory.CreateDirectory(dirpath);
                byte[] imageBytes = Convert.FromBase64String(base64Str);
                MemoryStream ms = new MemoryStream(imageBytes);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms);
                string fullPath = dirpath + imageName + ".png";
                image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                return imageName + ".png";
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static string Base64StringToImage(string dirpath, string imageName, string base64Str, int width, int height)
        {
            try
            {
                if (!Directory.Exists(dirpath))
                    Directory.CreateDirectory(dirpath);
                byte[] imageBytes = Convert.FromBase64String(base64Str);
                MemoryStream ms = new MemoryStream(imageBytes);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms);
                string fullPath = dirpath + imageName + ".png";
                Bitmap objNewPic = new System.Drawing.Bitmap(image, width, height);
                objNewPic.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                //image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                return imageName + ".png";
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static string Base64StringToImage(string dirpath, string imageName, string base64Str, int imgW, int imgH, int imgX1, int imgY1, int cropW, int cropH, float rotation)
        {
            try
            {
                if (!Directory.Exists(dirpath))
                    Directory.CreateDirectory(dirpath);
                byte[] imageBytes = Convert.FromBase64String(base64Str);
                MemoryStream ms = new MemoryStream(imageBytes);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms);
                string fullPath = dirpath + imageName + ".png";
                Bitmap origin = new System.Drawing.Bitmap(image, imgW, imgH);
                Bitmap newBmp = new Bitmap(cropW, cropH);
                Graphics g = Graphics.FromImage(newBmp);
                g.DrawImage(origin, new Rectangle(0, 0, cropW, cropH), new Rectangle(imgX1, imgY1, cropW, cropH), GraphicsUnit.Pixel);
                newBmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                return imageName + ".png";
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static bool FileExist(string filePath, string fileName)
        {
            try
            {
                return System.IO.File.Exists(filePath + fileName);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取实体类里面所有的名称、值、DESCRIPTION值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>  
        public string getProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name; //名称  
                object value = item.GetValue(t, null);  //值  
                string des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute))).Description;// 属性值  

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1}:{2},", name, value, des);
                }
                else
                {
                    getProperties(value);
                }
            }
            return tStr;
        }

        #region 获取任意日期所在周

        /// <summary>  
        /// 得到本周第一天(以星期天为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekFirstDaySun(DateTime datetime)
        {
            //星期天为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期六为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekLastDaySat(DateTime datetime)
        {
            //星期六为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (7 - weeknow) - 1;

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        #endregion

        /// <summary>
        /// 获取网络图片并保存
        /// </summary>
        /// <param name="imgURL">图片URL</param>
        /// <param name="time">超时时间（毫秒）</param>
        /// <param name="imgPath">保存路径</param>
        public Bitmap Get_img(string imgURL, int time, string imgPath)
        {
            Bitmap img = null;
            HttpWebRequest req;
            HttpWebResponse res = null;
            try
            {
                System.Uri httpUrl = new System.Uri(imgURL);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Timeout = time;
                //req.UserAgent = "";
                req.Accept = "*/*";
                req.Method = "GET";
                res = (HttpWebResponse)(req.GetResponse());
                img = new Bitmap(res.GetResponseStream());//获取图片流                 
                img.Save(imgPath + imgURL.Substring(imgURL.LastIndexOf("/")));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (res != null) res.Close();
            }
            return img;
        }

        #region 日期时间段处理
        /// <summary>
        /// 获取固定日期范围内的所有日期，以数组形式返回
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public static DateTime[] GetAllDays(DateTime startTime, DateTime endTime)
        {
            List<DateTime> listDay = new List<DateTime>();
            DateTime dtDay = new DateTime();
            //循环比较，取出日期；
            for (dtDay = startTime; dtDay.CompareTo(endTime) <= 0; dtDay = dtDay.AddDays(1))
            {
                listDay.Add(dtDay);
            }
            return listDay.ToArray();
        }

        #region 获取 本周、本月、本季度、本年 的开始时间或结束时间
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime? GetTimeStartByType(string TimeType, DateTime now)
        {
            switch (TimeType)
            {
                case "Week":
                    return now.AddDays(-(int)now.DayOfWeek + 1);
                case "Month":
                    return now.AddDays(-now.Day + 1);
                case "Season":
                    var time = now.AddMonths(0 - ((now.Month - 1) % 3));
                    return time.AddDays(-time.Day + 1);
                case "Year":
                    return now.AddDays(-now.DayOfYear + 1);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime? GetTimeEndByType(string TimeType, DateTime now)
        {
            switch (TimeType)
            {
                case "Week":
                    return now.AddDays(7 - (int)now.DayOfWeek);
                case "Month":
                    return now.AddMonths(1).AddDays(-now.AddMonths(1).Day + 1).AddDays(-1);
                case "Season":
                    var time = now.AddMonths((3 - ((now.Month - 1) % 3) - 1));
                    return time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1);
                case "Year":
                    var time2 = now.AddYears(1);
                    return time2.AddDays(-time2.DayOfYear);
                default:
                    return null;
            }
        }
        #endregion
        #endregion

        #region 生成验证码

        /// <summary>  
        /// 生成验证码  
        /// </summary>  
        /// <param name="length">指定验证码的长度</param>
        public string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值  
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字  
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字  
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码  
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }
        #endregion
    }

    public static class StringHelper
    {
        /// <summary>
        /// 画布元素转义字符串替换
        /// </summary>
        /// <param name="title">源字符串</param>
        /// <returns></returns>
        public static string CellTitleReplace(this string title)
        {
            return title.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\'").Replace("\t", "");
        }
    }
}
