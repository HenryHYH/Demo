using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using com.show.api.uti;

namespace com.show.api
{
    public class ShowApiRequest
    {
        private String appSecret;
        private int connectTimeout = 60000;//60秒
        private int readTimeout = 60000;//60秒
        private String url;



        private Hashtable textMap = new Hashtable();
        private Hashtable uploadMap = new Hashtable();
        private Hashtable headMap = new Hashtable();


        public ShowApiRequest(String url, String appid, String appSecret)
        {
            this.url = url;
            this.appSecret = appSecret;
            this.textMap.Add("showapi_appid", appid);
        }


        public Hashtable getTextMap()
        {
            return textMap;
        }
        public void setTextMap(Hashtable textMap)
        {
            this.textMap = textMap;
        }
        public String getUrl()
        {
            return url;
        }

        public String getAppSecret()
        {
            return appSecret;
        }
        public void setAppSecret(String appSecret)
        {
            this.appSecret = appSecret;
        }

        public Hashtable getUploadMap()
        {
            return uploadMap;
        }
        public void setUploadMap(Hashtable uploadMap)
        {
            this.uploadMap = uploadMap;
        }
        public Hashtable getHeadMap()
        {
            return headMap;
        }
        public void setHeadMap(Hashtable headMap)
        {
            this.headMap = headMap;
        }
        public int getConnectTimeout()
        {
            return connectTimeout;
        }
        public int getReadTimeout()
        {
            return readTimeout;
        }
        public ShowApiRequest setConnectTimeout(int connectTimeout)
        {
            this.connectTimeout = connectTimeout;
            return this;
        }
        public ShowApiRequest setReadTimeout(int readTimeout)
        {
            this.readTimeout = readTimeout;
            return this;
        }

        /**
         * 设置客户端与showapi网关的最大长连接数量。
         */
        public ShowApiRequest setUrl(String url)
        {
            this.url = url;
            return this;
        }


        /**
         * 添加post体的字符串参数
         */
        public ShowApiRequest addTextPara(String key, String value)
        {
            this.textMap.Add(key, value);
            return this;
        }

        /**
         * 添加post体的上传文件参数
         */
        public ShowApiRequest addFilePara(String key, String item)
        {
            this.uploadMap.Add(key, item);
            return this;
        }
        /**
         * 添加head头的字符串参数
         */
        public ShowApiRequest addHeadPara(String key, String value)
        {
            this.headMap.Add(key, value);
            return this;
        }

        public String post()
        {
            String res = "";
            try
            {
                res = ShowHttpHelper.post(this);
            }
            catch (Exception e)
            {
                res = ShowApiUtils.errorMsg(e.ToString());
            }
            return res;
        }

        public String get()
        {
            String res = "";
            try
            {
                res = ShowHttpHelper.get(this);
            }
            catch (Exception e)
            {
                res = ShowApiUtils.errorMsg(e.ToString());                
            }
            return res;
        }
    }
}
