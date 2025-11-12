using FluentValidation;
using FunctionConsultorio.Application.Shared.Validator;
using FunctionConsultorio.Application.UseCases.Agendas.CreateAgenda;
using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda;
using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgendaByTipoConsulta;
using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgendaPacienteByRecorrencia;
using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgendaPessoalByRecorrencia;
using FunctionConsultorio.Application.UseCases.Agendas.GetAllAgenda;
using FunctionConsultorio.Application.UseCases.Agendas.UpdateAgenda;
using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ConsultorioFunctions;

public class AgendaFunction
{
    private readonly ILogger<AgendaFunction> _logger;
    private readonly IMediator _mediator;
    public AgendaFunction(ILogger<AgendaFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function("GetAllAgenda")]
    public async Task<HttpResponseData> GetAgendas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllAgenda")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        GetAllAgendaRequest listaAgenda = new();

        var result = await _mediator.Send(listaAgenda, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("UpdateAgenda")]
    public async Task<HttpResponseData> UpdateAgenda([HttpTrigger(AuthorizationLevel.Function, "put", Route = "UpdateAgenda")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] UpdateAgendaRequest agendaRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new AgendaUpdateValidator();
        var validationResult = await validator.ValidateAsync(agendaRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados da agenda invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(agendaRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("CreateAgenda")]
    public async Task<HttpResponseData> CreateAgenda([HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreateAgenda")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] CreateAgendaRequest agendaRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new AgendaCreateValidator();
        var validationResult = await validator.ValidateAsync(agendaRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados de agenda invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(agendaRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("DeleteAgenda")]
    public async Task<HttpResponseData> DeleteAgenda([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteAgenda")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        DeleteAgendaRequest agenda = new(id);

        var result = await _mediator.Send(agenda, cancellationToken);

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

    [Function("DeleteAgendaByRecorrencia")]
    public async Task<HttpResponseData> DeleteAgendaByRecorrencia([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteAgendaByRecorrencia")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        DeleteAgendaPacienteByRecorrenciaRequest agenda = new(id);

        var result = await _mediator.Send(agenda, cancellationToken);

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

    [Function("DeleteAgendaPessoalByRecorrencia")]
    public async Task<HttpResponseData> DeleteAgendaPessoalByRecorrencia([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteAgendaPessoalByRecorrencia")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        DeleteAgendaPessoalByRecorrenciaRequest agenda = new(id);

        var result = await _mediator.Send(agenda, cancellationToken);

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

    [Function("DeleteByTipoConsulta")]
    public async Task<HttpResponseData> DeleteByTipoConsulta([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteByTipoConsulta")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["tipoConsulta"]!);

        DeleteAgendaByTipoConsultaRequest agendaRequest = new(id);

        var result = await _mediator.Send(agendaRequest, cancellationToken);

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