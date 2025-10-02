using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consultorio.Function;

public class PacienteFunction
{
    private readonly ILogger<PacienteFunction> _logger;

    public PacienteFunction(ILogger<PacienteFunction> logger)
    {
        _logger = logger;
    }

    [Function("PacienteFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions Pacientes!");
    }
}