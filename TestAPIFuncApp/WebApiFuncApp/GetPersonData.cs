using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WebApiFuncApp;

public class GetPersonData
{
    private readonly ILogger<GetPersonData> _logger;

    public GetPersonData(ILogger<GetPersonData> logger)
    {
        _logger = logger;
    }

    [Function("GetPersonData")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        /// take user input
        Console.Write("Enter a filename to list: ");
        string userInput = Console.ReadLine();
        userInput += " test";
        // shell command
        Process.Start("cmd.exe", "/C dir " + userInput);
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}