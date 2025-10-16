using FunctionConsultorio.Application.UseCases.Alertas.GetAllAlertasByMes;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ConsultorioFunctions;

public class AlertaFunction
{
    private readonly ILogger<AlertaFunction> _logger;
    private readonly IMediator _mediator;
    public AlertaFunction(ILogger<AlertaFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function("GetAllAlertasByMesAno")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllAlertasByMesAno")] HttpRequestData req, CancellationToken cancellationToken)
    {
        if (!TokenValidation.UnauthorizedResponse(req, out var message))
        {
            var responseError = req.CreateResponse(HttpStatusCode.OK);
            await responseError.WriteStringAsync(message);
            return responseError;
        }

        var queryParams = QueryHelpers.ParseQuery(req.Url.Query);

        int mes = int.Parse(queryParams["Mes"]!);
        int ano = int.Parse(queryParams["Ano"]!);

        GetAllAlertasByMesAnoRequest listaAlertas = new(mes, ano);

        var result = await _mediator.Send(listaAlertas, cancellationToken);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return response;
    }
}