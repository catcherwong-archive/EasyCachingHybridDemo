namespace App3.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITestService _service;

        public ValuesController(ITestService service)
        {
            this._service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await _service.TestCacheV1();

            await _service.TestCacheV2();

            await _service.TestCacheV3();
            
            return new string[] { "value1", "value2"};
        }      
    }
}
