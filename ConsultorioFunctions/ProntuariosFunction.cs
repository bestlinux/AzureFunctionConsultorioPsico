using FunctionConsultorio.Application.Shared.Validator;
using FunctionConsultorio.Application.UseCases.Pacientes.GetByIdPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamento;
using FunctionConsultorio.Application.UseCases.Prontuarios.GetAllProntuario;
using FunctionConsultorio.Application.UseCases.Prontuarios.GetAllProntuarioByPaciente;
using FunctionConsultorio.Application.UseCases.Prontuarios.UpdateProntuario;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ConsultorioFunctions;

public class ProntuariosFunction
{
    private readonly ILogger<ProntuariosFunction> _logger;
    private readonly IMediator _mediator;

    public ProntuariosFunction(ILogger<ProntuariosFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function("GetAllProntuarios")]
    public async Task<HttpResponseData> GetAllProntuarios([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllProntuarios")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        GetAllProntuarioRequest listaPronturario = new();

        var result = await _mediator.Send(listaPronturario, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("GetProntuarioByPaciente")]
    public async Task<HttpResponseData> GetProntuarioByPaciente([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetProntuarioByPaciente")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        GetAllProntuarioByPacienteRequest prontuario = new(id);

        var result = await _mediator.Send(prontuario, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("UpdateProntuario")]
    public async Task<HttpResponseData> UpdateProntuario([HttpTrigger(AuthorizationLevel.Function, "put", Route = "UpdateProntuario")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] UpdateProntuarioRequest pronturarioRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new ProntuarioUpdateValidator();
        var validationResult = await validator.ValidateAsync(pronturarioRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados de prontuario invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(pronturarioRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }
}