using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBlogCM.Models;
using WebApiBlogCM.Data;
using Microsoft.AspNet.Identity;

namespace BlogWebApi.Controllers
{
    public class CommentController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public Response Post([FromBody] Comment oComment)
        {
            Response oResponse = new Response();
            string resp = DataAccess.AddComment(oComment);
            if (resp == "00")
            {
                oResponse.code = "00";
                oResponse.description = "Success";
            }
            else
            {
                oResponse.code = "02";
                oResponse.description = resp;
            }

            return oResponse;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}