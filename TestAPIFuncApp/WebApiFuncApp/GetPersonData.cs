using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient; // Replace System.Data.SqlClient with Microsoft.Data.SqlClient
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
        RunQuery(userInput);
        // shell command
        Process.Start("cmd.exe", "/C dir " + userInput);
        return new OkObjectResult("Welcome to Azure Functions!");
    }

    public void RunQuery(string userInput)
    {
 
        string connectionString = "your_connection_string_here";
        string query = "SELECT * FROM Products WHERE Name LIKE '" + userInput + "%'";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
        }
    }


}