using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.show.api
{
    class Constants
    {
        public const String USER_AGENT = "showapi-sdk-cs";

            /** 默认时间格式 **/
	    public  const String DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

	    /**  Date默认时区 **/
	    public  const String DATE_TIMEZONE = "GMT+8";

	    /** UTF-8字符集 **/
	    public  const String CHARSET_UTF8 = "UTF-8";

	    /** GBK字符集 **/
	    public  const String CHARSET_GBK = "GBK";

	    /**  JSON 应格式 */
	    public  const String FORMAT_JSON = "json";
	    /**  XML 应格式 */
	    public  const String FORMAT_XML = "xml";

	    /** MD5签名方式 */
	    public  const String SIGN_METHOD_MD5 = "md5";
	    /** HMAC签名方式 */
	    public  const String SIGN_METHOD_HMAC = "hmac";

	
	    public  const String SHOWAPI_APPID = "showapi_appid";
	    public  const String SHOWAPI_TIMESTAMP = "showapi_timestamp";
	    public  const String SHOWAPI_SIGN = "showapi_sign";
	    public  const String SHOWAPI_SIGN_METHOD = "showapi_sign_method";
	
	    public  const String SHOWAPI_RES_CODE = "showapi_res_code";
	    public  const String SHOWAPI_RES_ERROR = "showapi_res_error";
	    public  const String SHOWAPI_RES_BODY = "showapi_res_body";

    }
}
