using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class BaiduIPHelper
    {
        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        public static string request(string url, string param)
        {
            string strURL = url + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", "36cdb8f338ee4cfddb53d75f55f7c6fb");
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

        public static BaiduIPMessage GetIpMsg(string ip)
        {
            var str = request("http://apis.baidu.com/apistore/iplookupservice/iplookup", "ip=" + ip);

            if (str.Contains("\"errNum\":0"))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<BaiduIPMessage>(str);
            }

            return null;
        }
    }

    public class BaiduIPMessage
    {
        public string ErrNum { get; set; }

        public string ErrMsg { get; set; }

        public BaiduIP RetData { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IP = " + RetData.IP);
            sb.AppendLine("Country = " + RetData.Country);
            sb.AppendLine("Province = " + RetData.Province);
            sb.AppendLine("City = " + RetData.City);
            sb.AppendLine("District = " + RetData.District);
            sb.AppendLine("Carrier = " + RetData.Carrier);

            return sb.ToString();
        }
    }

    public class BaiduIP
    {
        public string IP { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Carrier { get; set; }
    }
}
