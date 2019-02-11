namespace App1.Controllers
{
    using System;
    using System.Collections.Generic;
    using EasyCaching.Core;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHybridCachingProvider _hybrid;

        public ValuesController(IHybridCachingProvider hybrid)
        {
            this._hybrid = hybrid;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var res = _hybrid.Get<string>("cacheKey");

            return new string[] { "value1", "value2", res.Value };
        }

        // GET api/values/set
        [HttpGet("set")]
        public ActionResult<string> Set()
        {
            // the same key for different value of 
            _hybrid.Set("cacheKey", "val-from app1", TimeSpan.FromMinutes(1));

            return "ok";
        }
    }
}
