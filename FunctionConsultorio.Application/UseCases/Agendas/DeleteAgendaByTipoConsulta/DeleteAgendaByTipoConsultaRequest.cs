using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgendaByTipoConsulta
{
    public sealed record DeleteAgendaByTipoConsultaRequest(int TipoConsulta) : IRequest<DeleteAgendaResponse> { };
}
