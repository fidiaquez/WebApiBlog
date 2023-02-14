using WebApiBlogCM.Models;
using System;

namespace WebApiBlogCM.Data
{
    public interface IDataAccess
    {
        IPostResponse ListAllPosts();
        IPostResponse ListWriterPosts(string username);
        IPostResponse ListEditorPosts(string username);
        string AddPost(Post oPost);
    }
}