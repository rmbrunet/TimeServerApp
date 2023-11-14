using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimeServerApp;

public static class TimeServerFunction
{
    [FunctionName("TimeServer")]
    public static IActionResult Run(
       [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
       ClaimsPrincipal principal,
       ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var utc = DateTimeOffset.UtcNow;

        var claims = principal.Claims.Select(c => new { type = c.Type, value = c.Value }).ToList();

        return new OkObjectResult(new { Utc = utc, Claims = claims });
    }
}