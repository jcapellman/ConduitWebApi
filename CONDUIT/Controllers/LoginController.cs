﻿using System;
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

        // POST api/<controller>
        [HttpPost]
        public bool Post(int userId, string currentPassword, string newPassword)
        {
            using (var entities = new CONDUIT_Entities())
            {
                try
                {
                    entities.changePasswordSP(userId, currentPassword, newPassword);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}