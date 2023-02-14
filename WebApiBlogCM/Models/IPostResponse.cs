using System.Collections.Generic;

namespace WebApiBlogCM.Models
{
    public interface IPostResponse
    {
        string code { get; set; }
        string description { get; set; }
        List<Post> postList { get; set; }
    }
}