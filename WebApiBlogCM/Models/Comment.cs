using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBlogCM.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int postid { get; set; }
        public string description { get; set; }
        public string ispublic { get; set; }
        public DateTime creation_date { get; set; }
    }
}