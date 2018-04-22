using RestSharp.Deserializers;

namespace ConsoleApp
{
    /// <summary>
    /// 商标结果
    /// </summary>
    public class TrademarkDetailResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        [DeserializeAs(Name = "data")]
        public TrademarkDetailResultData Data { get; set; }
    }
}
