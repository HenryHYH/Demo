using RestSharp.Deserializers;

namespace ConsoleApp
{
    /// <summary>
    /// 商标流程
    /// </summary>
    public class TrademarkDetailResultDataFlow
    {
        /// <summary>
        /// 流程日期
        /// </summary>
        [DeserializeAs(Name = "flowDate")]
        public string FlowDate { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DeserializeAs(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [DeserializeAs(Name = "flowName")]
        public string FlowName { get; set; }
    }
}