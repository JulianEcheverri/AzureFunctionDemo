using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using static GitHubMonitorApp.GitHubBody;

namespace GitHubMonitorApp
{
    public static class GitHubMonitor
    {
        // Default route azure_url/api/{function_name}
        [FunctionName("GitHubMonitor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Out GitHub Monitor Processed an action");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Rootobject>(requestBody);

            log.LogInformation(requestBody);

            return new OkObjectResult(new { data.pusher.name, data.pusher.email });
        }
    }
}