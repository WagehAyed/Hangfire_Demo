
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController :ControllerBase
    {
    [HttpPost]
    [Route("CreateBackgroundJob")]
    public ActionResult CreateBackgroundJob(){
        BackgroundJob.Enqueue(()=> System.Console.WriteLine("Background job Triggered"));
        return Ok(); 
    }

    }
}