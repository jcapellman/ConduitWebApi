using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CONDUIT.DataLayer;

namespace CONDUIT.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public int Get(string username, string password)
        {
            using (var entities = new CONDUIT_Entities())
            {
                var result = entities.checkLoginSP(username, password).FirstOrDefault();

                if (result.HasValue) return result.Value;
                else return -1;
            }
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{

        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{

        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{

        //}
    }
}