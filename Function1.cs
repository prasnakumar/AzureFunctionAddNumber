using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AddNumber
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {

                int number1 = Convert.ToInt32(req.Query["number1"]);
                int number2 = Convert.ToInt32(req.Query["number2"]);

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);


                string responseMessage = $"Sum is  {number1 + number2}.";

                return new OkObjectResult(responseMessage);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult("Please enter a valid number");
            }
        }
    }
}
