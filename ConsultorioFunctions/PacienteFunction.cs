using Azure;
using ConsultorioFunctions;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading;

namespace Consultorio.Function;

public class PacienteFunction
{
    private readonly ILogger<PacienteFunction> _logger;
    private readonly IMediator _mediator;

    public PacienteFunction(ILogger<PacienteFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function("GetPacientes")]
    public async Task<HttpResponseData> GetPacientes([HttpTrigger(AuthorizationLevel.Function, "get", Route = "pacientes")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!req.Headers.TryGetValues("Authorization", out var authHeader))
        {
            var unauthorizedResponse = req.CreateResponse(HttpStatusCode.Unauthorized);
            await unauthorizedResponse.WriteStringAsync("Missing Authorization header.");
            return unauthorizedResponse;
        }
        if (!TokenValidation.ValidateToken(authHeader.FirstOrDefault()!))
        {
            var unauthorizedResponse = req.CreateResponse(HttpStatusCode.Unauthorized);
            await unauthorizedResponse.WriteStringAsync("Invalid or expired token.");
            return unauthorizedResponse;
        }
       
        GetAllPacienteRequest listaPaciente = new();

        var result = await _mediator.Send(listaPaciente, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }
}