using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt2_Group3.Models
{
    public class login
    {
    public int user_id { get; set; }

    public string username { get; set; }
    public string password { get; set; }
    public bool isEmployee { get; set; }
    }

    public class UserProfile
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
    }
}