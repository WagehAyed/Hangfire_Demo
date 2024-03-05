
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

[HttpPost]
[Route("CreateScheduledJob")]
public ActionResult CreateScheduledJob(){
var scheduledDateTime= DateTime.UtcNow.AddSeconds(5);
var dateTimeOffset=new DateTimeOffset(scheduledDateTime);
BackgroundJob.Schedule(() => System.Console.WriteLine("Scheduled Job Triggered"),dateTimeOffset);
return Ok();
}


[HttpPost]
[Route("CreateContinuationJob")]
public ActionResult CreateContinuationJob(){
var scheduledDateTime= DateTime.UtcNow.AddSeconds(5);
var dateTimeOffset=new DateTimeOffset(scheduledDateTime);
var jobId=BackgroundJob.Schedule(() => System.Console.WriteLine("Scheduled Job Triggered"),dateTimeOffset);

var job2Id=BackgroundJob.ContinueJobWith(jobId,()=> System.Console.WriteLine("Continuation Job 1 Triggered"));
BackgroundJob.ContinueJobWith(job2Id,()=> System.Console.WriteLine("Continuation Job 2 Triggered"));

return Ok();
}

[HttpPost]
[Route("CreateRecurringJob")]

public ActionResult CreateRecurringJob(){
RecurringJob.AddOrUpdate("RecurringJob1",() => System.Console.WriteLine("Recurring Job Triggered"),"* * * * *");
return Ok();
}

    }
}