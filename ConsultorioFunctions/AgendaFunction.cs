using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ConsultorioFunctions;

public class AgendaFunction
{
    private readonly ILogger<AgendaFunction> _logger;

    public AgendaFunction(ILogger<AgendaFunction> logger)
    {
        _logger = logger;
    }

    [Function("AgendaFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions Agenda!");
    }
}