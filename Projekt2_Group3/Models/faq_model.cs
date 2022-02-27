using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt2_Group3.Models
{
    public class faq_model
    {
        public Dictionary<String, List<knowledge>> data { get; set; }
        public String keyword { get; set; }
    }
}