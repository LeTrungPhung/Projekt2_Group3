//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projekt2_Group3
{
    using System;
    using System.Collections.Generic;
    
    public partial class invoice
    {
        public string invoice_id { get; set; }
        public System.DateTime purchase_date { get; set; }
        public decimal total_price { get; set; }
        public string clerk { get; set; }
        public Nullable<int> user_id { get; set; }
        public string article_id { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual portal_user portal_user { get; set; }
        public virtual article article { get; set; }
    }
}
