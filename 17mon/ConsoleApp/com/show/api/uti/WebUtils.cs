using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Policy;
using System.IO;

namespace com.show.api.uti
{
    class WebUtils
    {
        private const String DEFAULT_CHARSET = Constants.CHARSET_UTF8;
        private const String METHOD_POST = "POST";
        private const String METHOD_GET = "GET";

        private static String buildGetUrl(String strUrl, String query)
        {

            if (query == null || query.Trim().Length == 0)
            {
                return strUrl;
            }
            if (strUrl.EndsWith("?"))
            {
                strUrl = strUrl + query;
            }
            else
            {
                strUrl = strUrl + "?" + query;
            }
            return strUrl;
        }

        public static String buildQuery(Hashtable myparams)
        {
            if (myparams == null || myparams.Count == 0)
            {
                return null;
            }
            StringBuilder query = new StringBuilder();
            bool hasParam = false;

            foreach (DictionaryEntry de in myparams)
            {
                String k = de.Key.ToString();
                String v = de.Value.ToString();

                if (k != null && k.Trim().Length > 0 && v != null && v.Trim().Length > 0)
                {
                    if (hasParam)
                    {
                        query.Append("&");
                    }
                    else
                    {
                        hasParam = true;
                    }

                    query.Append(k).Append("=").Append(HttpUtility.UrlEncode(v, Encoding.UTF8));
                }
            }
            return query.ToString();
        }

        public static String doGet(String url, Hashtable myparams, int connectTimeout, int readTimeout)
        {

            try
            {

                String ctype = "application/x-www-form-urlencoded;charset=" + DEFAULT_CHARSET;
                String query = buildQuery(myparams);

                HttpWebRequest request = WebRequest.Create(buildGetUrl(url, query)) as HttpWebRequest;
                request.Method = "GET";

                request.ReadWriteTimeout = readTimeout;
                request.Timeout = connectTimeout;

                request.ContentType = ctype;
                request.UserAgent = Constants.USER_AGENT;
                request.Accept = "text/xml,text/javascript";

                //cookie使用set head替代
                //if (cookies != null)  
                //{  
                //    request.CookieContainer = new CookieContainer();  
                //    request.CookieContainer.Add(cookies);  
                //}  
                HttpWebResponse resp = request.GetResponse() as HttpWebResponse;

                return Resp2String(resp);
            }
            catch (Exception e)
            {
                throw e;
            }


        }
        public static String doPost(String url, Hashtable myparams, Hashtable fileParams,
                Hashtable headerMap, int connectTimeout, int readTimeout)
        {
            if (fileParams == null || fileParams.Count == 0)
            {
                return _doPost(url, myparams, headerMap, connectTimeout, readTimeout);
            }
            else
            {
                return _doPostWithFile(url, myparams, fileParams, headerMap, connectTimeout, readTimeout);
            }
        }

        public static String _doPost(String url, Hashtable myparams,
                Hashtable headerMap, int connectTimeout, int readTimeout)
        {

            HttpWebRequest request = null;
            Stream stream = null;//用于传参数的流  

            try
            {
                //如果是发送HTTPS请求    
                /*
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    //创建证书文件  
                    System.Security.Cryptography.X509Certificates.X509Certificate objx509 = new System.Security.Cryptography.X509Certificates.X509Certificate(
                        //Application.StartupPath + @"\\licensefile\zjs.cer"
                        );
                    //添加到请求里  
                    request.ClientCertificates.Add(objx509);
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                                */


                request = WebRequest.Create(url) as HttpWebRequest;

                request.Method = "POST";//传输方式  

                request.ReadWriteTimeout = readTimeout;
                request.Timeout = connectTimeout;

                request.ContentType = "application/x-www-form-urlencoded;charset=" + DEFAULT_CHARSET;
                request.UserAgent = Constants.USER_AGENT;
                request.Accept = "text/xml,text/javascript";


                //cookie使用set head替代
                //if (cookies != null)
                //{
                //    request.CookieContainer = new CookieContainer();
                //    request.CookieContainer.Add(cookies);
                //}

                if (headerMap != null)
                {
                    foreach (DictionaryEntry de in headerMap)
                    {
                        String k = de.Key.ToString();
                        String v = de.Value.ToString();
                        request.Headers.Set(k, v);
                    }
                }

                //如果需求POST传数据，转换成utf-8编码  
                String query = buildQuery(myparams);
                byte[] data = Encoding.UTF8.GetBytes(query);
                request.ContentLength = data.Length;

                stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                stream = null;

                HttpWebResponse resp = request.GetResponse() as HttpWebResponse;

                return Resp2String(resp);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }


        private static byte[] getTextEntry(String fieldName, String fieldValue)
        {
            StringBuilder entry = new StringBuilder();
            entry.Append("Content-Disposition:form-data;name=\"");
            entry.Append(fieldName);
            entry.Append("\"\r\nContent-Type:text/plain\r\n\r\n");
            entry.Append(fieldValue);
            return Encoding.UTF8.GetBytes(entry.ToString());
        }
        private static byte[] getFileEntry(String fieldName, String fileName, String mimeType)
        {
            StringBuilder entry = new StringBuilder();
            entry.Append("Content-Disposition:form-data;name=\"");
            entry.Append(fieldName);
            entry.Append("\";filename=\"");
            entry.Append(fileName);
            entry.Append("\"\r\nContent-Type:");
            entry.Append(mimeType);
            entry.Append("\r\n\r\n");
            return Encoding.UTF8.GetBytes(entry.ToString());
        }
        public static String _doPostWithFile(string url, Hashtable myparas, Hashtable files, Hashtable headerMap, int connectTimeout, int readTimeout)
        {
            String ret = "";
            Stream stream = null;
            try
            {
                string boundary = "----------------------------" +
                DateTime.Now.Ticks.ToString("x");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";

                request.ReadWriteTimeout = readTimeout;
                request.Timeout = connectTimeout;

                request.ContentType = "multipart/form-data;charset=" + DEFAULT_CHARSET + " ;boundary=" + boundary;
                request.UserAgent = Constants.USER_AGENT;
                request.Accept = "text/xml,text/javascript";

                if (headerMap != null)
                {
                    foreach (DictionaryEntry de in headerMap)
                    {
                        String k = de.Key.ToString();
                        String v = de.Value.ToString();
                        request.Headers.Set(k, v);
                    }
                }


                request.KeepAlive = true;
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;

                stream = request.GetRequestStream();

                // 组装文本请求参数
                byte[] entryBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                foreach (DictionaryEntry de in myparas)
                {
                    String k = de.Key.ToString();
                    String v = de.Value.ToString();
                    byte[] textBytes = getTextEntry(k, v);
                    stream.Write(entryBoundaryBytes, 0, entryBoundaryBytes.Length);
                    stream.Write(textBytes, 0, textBytes.Length);
                }
                // 组装文件请求参数
                foreach (DictionaryEntry de in files)
                {
                    String k = de.Key.ToString();
                    String v = de.Value.ToString();

                    //TODO:mimetype暂时先写死 20150601 zl

                    byte[] fileBytes = getFileEntry(k, v, "application/octet-stream");
                    stream.Write(entryBoundaryBytes, 0, entryBoundaryBytes.Length);
                    stream.Write(fileBytes, 0, fileBytes.Length);

                    FileStream fileStream = new FileStream(v, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1024];

                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }
                }

                // 添加请求结束标志
                byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                stream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                stream.Close();

                HttpWebResponse resp = request.GetResponse() as HttpWebResponse;

                return Resp2String(resp);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static string Resp2String(HttpWebResponse HttpWebResponse)
        {
            Stream responseStream = null;
            StreamReader sReader = null;
            String value = null;

            try
            {
                responseStream = HttpWebResponse.GetResponseStream();

                sReader = new StreamReader(responseStream, Encoding.GetEncoding(DEFAULT_CHARSET));
                value = sReader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sReader != null)
                    sReader.Close();

                if (responseStream != null)
                    responseStream.Close();

                if (HttpWebResponse != null)
                    HttpWebResponse.Close();
            }

            return value;
        }

    }

}
