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
    
    public partial class T_Settlement
    {
        public string FSettlementNo { get; set; }
        public int FTaskType { get; set; }
        public string FBankId { get; set; }
        public string FInstitutionId { get; set; }
        public string FInterFaceSn { get; set; }
        public string FProjectCode { get; set; }
        public int FProjectType { get; set; }
        public decimal FAmount { get; set; }
        public int FOrderDetailType { get; set; }
        public int FOrderType { get; set; }
        public int FSettlementType { get; set; }
        public string FAccountNumber { get; set; }
        public Nullable<int> FStatus { get; set; }
        public string FRemark { get; set; }
        public Nullable<System.DateTime> FCreateTime { get; set; }
        public string FBizType { get; set; }
        public string FAmountWhitelistID { get; set; }
        public string FResponseMessage { get; set; }
    }
}
