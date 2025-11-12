using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgendaPessoalByRecorrencia
{
    public sealed record DeleteAgendaPessoalByRecorrenciaRequest(int CategoriaAgendamento) : IRequest<DeleteAgendaResponse>;
}
