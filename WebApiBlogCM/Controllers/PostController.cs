using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBlogCM.Models;
using WebApiBlogCM.Data;
using Microsoft.AspNet.Identity;
using Autofac;


namespace WebApiBlogCM.Controllers
{
     [Authorize]
    public class PostController : ApiController
    {
        private IDataAccess _dataAccess;
        private IPostResponse _oPostResponse;
        private IResponse _oResponse;
        private IPost _oPost;

        public PostController(IDataAccess dataAccess, IPostResponse oPostResponse,IResponse oResponse)
        {         
            _dataAccess = dataAccess;
            _oPostResponse = oPostResponse;
            _oResponse = oResponse;

        }

        // GET api/<controller>
        public IPostResponse Get()
        {
            _oPostResponse = _dataAccess.ListAllPosts();
            return _oPostResponse;

        }

        // GET api/<controller>/5
        //public PostResponse Get(string id, [FromBody] Post oPost)
        public IPostResponse Get(string id)
        {
            string username = RequestContext.Principal.Identity.GetUserName();
                    
            if (id == "listwriterposts")
            {
                _oPostResponse = _dataAccess.ListWriterPosts(username);
            }

            else if (id == "listeditorposts")
            {
                _oPostResponse = _dataAccess.ListEditorPosts(username);
            }
            return _oPostResponse;


        }

        // POST api/<controller>
        public IResponse Post([FromBody] Post oPost )
        {
                //Response oResponse = new Response();
         
                string resp = _dataAccess.AddPost(oPost);            
          
               // string resp = "";
                if (resp == "00")
                {
                    _oResponse.code = "00";
                    _oResponse.description = "Success";
                }
                else if (resp == "01")
                {
                    _oResponse.code = "01";
                    _oResponse.description = "User not authorized, not a writer.";
                }
                else
                {
                    _oResponse.code = "02";
                    _oResponse.description = resp;
                }
            
            return _oResponse;

        }

        // PUT api/<controller>/5
        public IResponse Put(string id, [FromBody] Post oPost)
        {
            
            string resp = "";
            if (id == "updatepost")
            {
                resp = DataAccess.UpdatePost(oPost);
            }
            else if (id == "submitpost")
            {
                resp = DataAccess.SubmitPost(oPost);
            }
            else if (id == "rejectpost")
            {
                resp = DataAccess.RejectPost(oPost);
            }
            else if (id == "publishpost")
            {
                resp = DataAccess.PublishPost(oPost);
            }

            if (resp == "00")
            {
                _oResponse.code = "00";
                _oResponse.description = "Success";
            }
            else if (resp == "01")
            {
                _oResponse.code = "01";
                _oResponse.description = "User not authorized";
            }
            else
            {
                _oResponse.code = "02";
                _oResponse.description = resp;
            }


            return _oResponse;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}