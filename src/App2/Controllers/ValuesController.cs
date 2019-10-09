namespace App2.Controllers
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

        public ValuesController(IHybridProviderFactory hybridFactory)
        {
            this._hybrid = hybridFactory.GetHybridCachingProvider("h1");
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
            _hybrid.Set("cacheKey", "val--from app2", TimeSpan.FromMinutes(1));

            return "ok";
        }
    }
}
