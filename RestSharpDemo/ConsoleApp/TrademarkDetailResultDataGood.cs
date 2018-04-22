using RestSharp.Deserializers;

namespace ConsoleApp
{
    /// <summary>
    /// 商标使用商品
    /// </summary>
    public class TrademarkDetailResultDataGood
    {
        /// <summary>
        /// 群组号
        /// </summary>
        [DeserializeAs(Name = "goodsCode")]
        public string GoodsCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DeserializeAs(Name = "goodsName")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        [DeserializeAs(Name = "beDeleted")]
        public string BeDeleted { get; set; }
    }
}