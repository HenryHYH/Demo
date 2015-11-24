using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

namespace com.show.api.uti
{
    class ShowApiUtils
    {

        public static String errorMsg(String msg)
        {
            //简单粗暴的替换一下json关键字符
            msg = msg.Replace("\"", "").Replace(":","").Replace("'","");
            String str = "{" + Constants.SHOWAPI_RES_CODE + ":-1," + Constants.SHOWAPI_RES_ERROR + ":\"" + msg + "\"," + Constants.SHOWAPI_RES_BODY + ":{}}";
            return str;
        }

        /**
	 * 给请求签名。
	 * @param params 所有字符型的请求参数
	 * @param secret 签名密钥
	 * @param isHmac 是否为HMAC方式加密
	 * @return 签名
	 * @throws IOException
	 */
	public static String signRequest(Hashtable myparams, String secret, Boolean isHmac)  {
		
        
        //1.字典排序
	    SortedSet<String> keys=new SortedSet<String>();		        
        foreach(DictionaryEntry de in myparams)
        {  
            String k=de.Key.ToString();
			//Object v=de.Value;            
            keys.Add(k);
        }  

       
		//2.把所有参数名和参数值串在一起
		StringBuilder query = new StringBuilder();
		foreach (String key in keys) {
            String value = myparams[key] + "";
            if (key != null && key.Trim().Length > 0 && value != null && value.Trim().Length > 0)
            {
				query.Append(key).Append(value);
			}
		}
		if (!isHmac) {
			query.Append(secret);
		}

		// 3.使用MD5/HMAC加密
		byte[] bytes=null;
		if (isHmac) {
            //bytes = encryptHMAC(query.toString(), secret);
		} else {
			bytes = encryptMD5(query.ToString());
		}
		

		// 4.把二进制转化为大写的十六进制
		return byte2hex(bytes);
	}

    //private static byte[] encryptHMAC(String data, String secret) throws IOException {
    //    byte[] bytes = null;
    //    try {
    //        SecretKey secretKey = new SecretKeySpec(secret.getBytes(Constants.CHARSET_UTF8), "HmacMD5");
    //        Mac mac = Mac.getInstance(secretKey.getAlgorithm());
    //        mac.init(secretKey);
    //        bytes = mac.doFinal(data.getBytes(Constants.CHARSET_UTF8));
    //    } catch (GeneralSecurityException gse) {
    //        String msg=getStringFromException(gse);
    //        throw new IOException(msg);
    //    }
    //    return bytes;
    //}

    //private static String getStringFromException(Throwable e) {
    //    String result = "";
    //    ByteArrayOutputStream bos = new ByteArrayOutputStream();
    //    PrintStream ps = new PrintStream(bos);
    //    e.printStackTrace(ps);
    //    try {
    //        result = bos.toString(Constants.CHARSET_UTF8);
    //    } catch (IOException ioe) {
    //    }
    //    return result;
    //}

	private static byte[] encryptMD5(String data)  {
		byte[] bytes = null;
		
        MD5 md5Hasher = MD5.Create();

        //bytes = md.digest(data.getBytes(Constants.CHARSET_UTF8));
        bytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
            
		
		return bytes;
	}

	private static String byte2hex(byte[] bytes) {
		StringBuilder sign = new StringBuilder();
		for (int i = 0; i < bytes.Length; i++) {
            //String hex =  Integer.toHexString(bytes[i] & 0xFF);
            String hex = (bytes[i] & 0xFF).ToString("X2");
            if (hex.Length == 1) {
				sign.Append("0");
			}
			sign.Append(hex.ToUpper());
		}
		return sign.ToString();
	}

	/**
	 * 获取文件的真实后缀名。目前只支持JPG, GIF, PNG, BMP四种图片文件。
	 * 
	 * @param bytes 文件字节流
	 * @return JPG, GIF, PNG or null
	 */
	public static String getFileSuffix(byte[] bytes) {
		if (bytes == null || bytes.Length < 10) {
			return null;
		}

		if (bytes[0] == 'G' && bytes[1] == 'I' && bytes[2] == 'F') {
			return "GIF";
		} else if (bytes[1] == 'P' && bytes[2] == 'N' && bytes[3] == 'G') {
			return "PNG";
		} else if (bytes[6] == 'J' && bytes[7] == 'F' && bytes[8] == 'I' && bytes[9] == 'F') {
			return "JPG";
		} else if (bytes[0] == 'B' && bytes[1] == 'M') {
			return "BMP";
		} else {
			return null;
		}
	}

	/**
	 * 获取文件的真实媒体类型。目前只支持JPG, GIF, PNG, BMP四种图片文件。
	 * 
	 * @param bytes 文件字节流
	 * @return 媒体类型(MEME-TYPE)
	 */
	public static String getMimeType(byte[] bytes) {
		String suffix = getFileSuffix(bytes);
		String mimeType;

		if ("JPG".Equals(suffix)) {
			mimeType = "image/jpeg";
		} else if ("GIF".Equals(suffix)) {
			mimeType = "image/gif";
		} else if ("PNG".Equals(suffix)) {
			mimeType = "image/png";
		} else if ("BMP".Equals(suffix)) {
			mimeType = "image/bmp";
		}else {
			mimeType = "application/octet-stream";
		}

		return mimeType;
	}
    }
}
