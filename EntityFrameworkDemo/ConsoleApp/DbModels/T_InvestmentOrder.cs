//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp.DbModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_InvestmentOrder
    {
        public string FInvestmentOrderNo { get; set; }
        public string FBankId { get; set; }
        public string FInstitutionId { get; set; }
        public string FInterFaceSn { get; set; }
        public string FAccountUserNo { get; set; }
        public string FProjectCode { get; set; }
        public int FPaymentType { get; set; }
        public decimal FTotalAmount { get; set; }
        public decimal FInterestRate { get; set; }
        public int FDuration { get; set; }
        public System.DateTime FExpireTime { get; set; }
        public Nullable<int> FStatus { get; set; }
        public Nullable<System.DateTime> FCreateTime { get; set; }
        public string FBizType { get; set; }
    }
}
