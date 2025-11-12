using Azure;
using Azure.Core;
using ConsultorioFunctions;
using FunctionConsultorio.Application.Shared.Validator;
using FunctionConsultorio.Application.UseCases.Alertas.GetAllAlertasByMes;
using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetByIdPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

    [Function("UpdatePaciente")]
    public async Task<HttpResponseData> UpdatePaciente([HttpTrigger(AuthorizationLevel.Function, "put", Route = "UpdatePaciente")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] UpdatePacienteRequest pacienteRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new PacienteUpdateValidator();
        var validationResult = await validator.ValidateAsync(pacienteRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados de paciente invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(pacienteRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("CreatePaciente")]
    public async Task<HttpResponseData> CreatePaciente([HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreatePaciente")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] CreatePacienteRequest pacienteRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new PacienteCreateValidator();
        var validationResult = await validator.ValidateAsync(pacienteRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados de paciente invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(pacienteRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("GetAllPacientes")]
    public async Task<HttpResponseData> GetPacientes([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllPacientes")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        GetAllPacienteRequest listaPaciente = new();

        var result = await _mediator.Send(listaPaciente, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }


    [Function("GetPacienteById")]
    public async Task<HttpResponseData> GetPacienteById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetPacienteById")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        GetByIdPacienteRequest paciente = new(id);

        var result = await _mediator.Send(paciente, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("DeletePaciente")]
    public async Task<HttpResponseData> DeletePaciente([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeletePaciente")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        DeletePacienteRequest paciente = new(id);

        var result = await _mediator.Send(paciente, cancellationToken);

        if (result.Success.HasValue && !result.Success.Value)
        {
            var response = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
            return response;
        }
        else
        {
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
            return response;
        }
    }
}