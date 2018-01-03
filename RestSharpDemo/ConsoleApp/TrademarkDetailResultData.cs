using RestSharp.Deserializers;
using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// 商标数据
    /// </summary>
    public class TrademarkDetailResultData
    {
        /// <summary>
        /// 接口在本日内剩余次数
        /// </summary>
        [DeserializeAs(Name = "remainCount")]
        public int RemainCount { get; set; }

        /// <summary>
        /// 商标图片名
        /// </summary>
        [DeserializeAs(Name = "tmImg")]
        public string TrademarkImg { get; set; }

        /// <summary>
        /// 注册号
        /// </summary>
        [DeserializeAs(Name = "regNo")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 国际分类
        /// </summary>
        [DeserializeAs(Name = "intCls")]
        public string Classification { get; set; }

        /// <summary>
        /// 商标名
        /// </summary>
        [DeserializeAs(Name = "tmName")]
        public string TrademarkName { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        [DeserializeAs(Name = "appDate")]
        public string ApplyDate { get; set; }

        /// <summary>
        /// 申请人名称中文
        /// </summary>
        [DeserializeAs(Name = "applicantCn")]
        public string ApplicantCn { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DeserializeAs(Name = "idCardNo")]
        public string IdCardNo { get; set; }

        /// <summary>
        /// 申请人地址中文
        /// </summary>
        [DeserializeAs(Name = "addressCn")]
        public string AddressCn { get; set; }

        /// <summary>
        /// 共有申请人1
        /// </summary>
        [DeserializeAs(Name = "applicantOther1")]
        public string ApplicantOther1 { get; set; }

        /// <summary>
        /// 共有申请人2
        /// </summary>
        [DeserializeAs(Name = "applicantOther2")]
        public string ApplicantOther2 { get; set; }

        /// <summary>
        /// 申请人名称英文
        /// </summary>
        [DeserializeAs(Name = "applicantEn")]
        public string ApplicantEn { get; set; }

        /// <summary>
        /// 申请人地址英文
        /// </summary>
        [DeserializeAs(Name = "addressEn")]
        public string AddressEn { get; set; }

        /// <summary>
        /// 初审公告期号
        /// </summary>
        [DeserializeAs(Name = "announcementIssue")]
        public string AnnouncementIssue { get; set; }

        /// <summary>
        /// 初审公告日期
        /// </summary>
        [DeserializeAs(Name = "announcementDate")]
        public string AnnouncementDate { get; set; }

        /// <summary>
        /// 注册公告期号
        /// </summary>
        [DeserializeAs(Name = "regIssue")]
        public string RegisterIssue { get; set; }

        /// <summary>
        /// 注册公告日期
        /// </summary>
        [DeserializeAs(Name = "regDate")]
        public string RegisterDate { get; set; }

        /// <summary>
        /// 专用权期限
        /// </summary>
        [DeserializeAs(Name = "privateDate")]
        public string PrivateDate { get; set; }

        /// <summary>
        /// 代理人
        /// </summary>
        [DeserializeAs(Name = "agent")]
        public string Agent { get; set; }

        /// <summary>
        /// 商标类型，一般、特殊、集体、证明
        /// </summary>
        [DeserializeAs(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// 后期指定日期
        /// </summary>
        [DeserializeAs(Name = "hqzdrq")]
        public string HouQiZhiDingRiQi { get; set; }

        /// <summary>
        /// 国际注册日期
        /// </summary>
        [DeserializeAs(Name = "gjzcrq")]
        public string InternationalRegisterDate { get; set; }

        /// <summary>
        /// 优先权日期
        /// </summary>
        [DeserializeAs(Name = "yxqrq")]
        public string PriorityDate { get; set; }

        /// <summary>
        /// 指定颜色
        /// </summary>
        [DeserializeAs(Name = "color")]
        public string Color { get; set; }

        /// <summary>
        /// 使用商品
        /// </summary>
        [DeserializeAs(Name = "goods")]
        public List<TrademarkDetailResultDataGood> Goods { get; set; }

        /// <summary>
        /// 商标流程
        /// </summary>
        [DeserializeAs(Name = "flow")]
        public List<TrademarkDetailResultDataFlow> Flow { get; set; }
    }
}