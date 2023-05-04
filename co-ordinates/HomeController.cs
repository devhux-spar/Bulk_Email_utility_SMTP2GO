using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace co_ordinates
{
    [Route("[controller]")]
    [ApiController]
    public class messagesController : ControllerBase
    {
        static Singleton singleton = Singleton.Instance;

        private List<Msg> _Msg = new List<Msg>
        {          
            new Msg { Count =  singleton.count, TotalCount = singleton.totalcount, Message = singleton.countstr }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_Msg);
        }
        

    }
}
