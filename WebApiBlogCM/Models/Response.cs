using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBlogCM.Models
{
    public class Response : IResponse
    {
        public string code { get; set; }
        public string description { get; set; }

    }
}