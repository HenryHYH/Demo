using System;
using System.Text;
using System.Web;
namespace tenpayApp
{
    /// <summary>
    /// TenpayUtil ��ժҪ˵����
    /// </summary>
    public class TenpayUtil
    {
        public static string tenpay = "1";
        public static string bargainor_id = "1900000113";                   //�Ƹ�ͨ�̻���
        public static string tenpay_key = "e82573dc7e6136ba414f2e2affbe39fa";  					//�Ƹ�ͨ��Կ;
        public static string tenpay_return = "http://********/payReturnUrl.aspx";//��ʾ֧��֪ͨҳ��;
        public static string tenpay_notify = "http://*****/payReturnUrl.aspx"; //֧����ɺ�Ļص�����ҳ��;

        public TenpayUtil()
        {
            /*tenpay      = System.Configuration.ConfigurationSettings.AppSettings["tenpay"];
            bargainor_id = System.Configuration.ConfigurationSettings.AppSettings["bargainor_id"];
            tenpay_key = System.Configuration.ConfigurationSettings.AppSettings["tenpay_key"];
            tenpay_return = System.Configuration.ConfigurationSettings.AppSettings["tenpay_return"];
            tenpay_show = System.Configuration.ConfigurationSettings.AppSettings["tenpay_show"];*/
        }
        /** ���ַ�������URL���� */
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;
            }
        }

        /** ���ַ�������URL���� */
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;

            }
        }

        /** ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ�� */
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        /** ȡ����� */
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }
    }
}