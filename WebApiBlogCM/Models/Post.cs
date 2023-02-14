using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBlogCM.Models
{
    public class Post : IPost
    {
        public int id { get; set; }
        public int writerid { get; set; }
        public int editorid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string submitted { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime last_update_date { get; set; }


    }
}