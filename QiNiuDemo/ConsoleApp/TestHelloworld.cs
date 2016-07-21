using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qiniu.Conf;
using Qiniu.IO;
using Qiniu.RS;
using Xunit;
using XunitShouldExtension;

namespace ConsoleApp
{
    public class TestHelloworld
    {
        [Fact]
        public void Test()
        {
            //设置账号的AK和SK
            Config.ACCESS_KEY = "8NXAoPOInRsnTVomjBKUC3KHCvfWfZFqRLenOANR";
            Config.SECRET_KEY = "xyQ7wcbLukoGUFFCV814PRU9HSf9S3gniEKs6E9M";

            //设置上传的空间
            string bucket = "helloworld";

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy put = new PutPolicy(bucket, 3600);

            //调用Token()方法生成上传的Token
            string upToken = put.Token();

            //string upToken = @"APZpifSHJlh0TYB2UJRm5WXyo_zB4cF5oyUFrwu4:Os7lv0cv3ngPdcp3hAEyAWkpspU=:eyJzY29wZSI6InJvbmciLCJkZWFkbGluZSI6MTQ2OTA2OTM4NCwiaW5zZXJ0T25seSI6MCwiZGV0ZWN0TWltZSI6MCwiZnNpemVMaW1pdCI6MCwiZnNpemVNaW4iOjAsImNhbGxiYWNrRmV0Y2hLZXkiOjB9";
            ////string upToken = @"APZpifSHJlh0TYB2UJRm5WXyo_zB4cF5oyUFrwu4:dIynZd9Ln8Vz43x2Eo6j-Ugy5IA=:eyJzY29wZSI6InJvbmciLCJkZWFkbGluZSI6MTQ2OTA3MDEyMSwiaW5zZXJ0T25seSI6MCwiZGV0ZWN0TWltZSI6MCwiZnNpemVMaW1pdCI6MCwiZnNpemVNaW4iOjAsImNhbGxiYWNrRmV0Y2hLZXkiOjB9";

            //上传文件的路径
            string filePath = "UploadFiles/Helloworld.txt";
            //设置上传的文件的key值
            string key = "test_helloworld4.txt";

            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            //调用PutFile()方法上传
            PutRet ret = target.PutFile(upToken, key, filePath, extra);

            ret.OK.ShouldBe(true);

            //打印出相应的信息
            //var response = ret.Response.ToString();
            //var retKey = ret.key;
        }
    }
}
