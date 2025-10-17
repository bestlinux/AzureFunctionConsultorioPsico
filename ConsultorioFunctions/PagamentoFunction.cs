using FluentValidation;
using FunctionConsultorio.Application.Shared.Validator;
using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
using FunctionConsultorio.Application.UseCases.Pagamentos.CreatePagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.DeletePagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamentoByPacienteMesAno;
using FunctionConsultorio.Application.UseCases.Pagamentos.UpdatePagamento;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;

namespace ConsultorioFunctions;

public class PagamentoFunction
{
    private readonly ILogger<PagamentoFunction> _logger;
    private readonly IMediator _mediator;

    public PagamentoFunction(ILogger<PagamentoFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function("DeletePagamento")]
    public async Task<HttpResponseData> DeletePagamento([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeletePagamento")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["Id"]!);

        DeletePagamentoRequest pagamento = new(id);

        var result = await _mediator.Send(pagamento, cancellationToken);

        if (!result)
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

    [Function("CreatePagamento")]
    public async Task<HttpResponseData> CreatePagamento([HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreatePagamento")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] CreatePagamentoRequest pagamentoRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new PagamentoCreateValidator();
        var validationResult = await validator.ValidateAsync(pagamentoRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados de pagamento invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(pagamentoRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("GetAllPagamentos")]
    public async Task<HttpResponseData> GetAllPagamentos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllPagamentos")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        GetAllPagamentoRequest listaPagamento = new();

        var result = await _mediator.Send(listaPagamento, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("GetPagamentosByPacienteMesAno")]
    public async Task<HttpResponseData> GetAllPagamentoByPacienteMesAno([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetPagamentosByPacienteMesAno")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int id = int.Parse(queryParams["PacienteID"]!);
        int mes = int.Parse(queryParams["Mes"]!);
        int ano = int.Parse(queryParams["Ano"]!);

        GetAllPagamentoByPacienteMesAnoRequest request = new(id,mes,ano);


        var result = await _mediator.Send(request, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }

    [Function("UpdatePagamento")]
    public async Task<HttpResponseData> UpdatePagamento([HttpTrigger(AuthorizationLevel.Function, "put", Route = "UpdatePagamento")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] UpdatePagamentoRequest updatePagamentoRequest, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var validator = new PagamentoUpdateValidator();
        var validationResult = await validator.ValidateAsync(updatePagamentoRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Dados de pagamento invalidos" });
            return unauthorizedResponse;
        }

        var result = await _mediator.Send(updatePagamentoRequest, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }
}