//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FenixSendCdlManually
{
    using System;
    using System.Collections.Generic;
    
    public partial class cdlItems
    {
        public int ID { get; set; }
        public string GroupGoods { get; set; }
        public string Code { get; set; }
        public string DescriptionCz { get; set; }
        public string DescriptionEng { get; set; }
        public int MeasuresId { get; set; }
        public int ItemTypesId { get; set; }
        public Nullable<int> PackagingId { get; set; }
        public string ItemType { get; set; }
        public string PC { get; set; }
        public string Packaging { get; set; }
        public bool IsSent { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public int ModifyUserId { get; set; }
        public Nullable<int> GroupsId { get; set; }
        public string ItemTypeDesc1 { get; set; }
        public string ItemTypeDesc2 { get; set; }
    }
}
