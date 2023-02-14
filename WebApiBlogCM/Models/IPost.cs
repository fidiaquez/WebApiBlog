using System;

namespace WebApiBlogCM.Models
{
    public interface IPost
    {
         int id { get; set; }
         int writerid { get; set; }
         int editorid { get; set; }
         string title { get; set; }
         string description { get; set; }
         string status { get; set; }
         string submitted { get; set; }
         DateTime creation_date { get; set; }
         DateTime last_update_date { get; set; }
    }
}