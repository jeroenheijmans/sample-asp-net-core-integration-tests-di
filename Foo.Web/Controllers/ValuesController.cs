using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Foo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBarService barService;

        public ValuesController(IBarService barService)
        {
            this.barService = barService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { barService.GetValue() };
        }
    }
}
