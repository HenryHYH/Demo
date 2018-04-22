using RestSharp.Deserializers;

namespace ConsoleApp
{
    /// <summary>
    /// 商标信息查询数据接口结果
    /// </summary>
    public class TrademarkDetail
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [DeserializeAs(Name = "error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [DeserializeAs(Name = "reason")]
        public string Reason { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        [DeserializeAs(Name = "result")]
        public TrademarkDetailResult Result { get; set; }
    }
}
