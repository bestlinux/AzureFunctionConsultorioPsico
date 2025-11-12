using FunctionConsultorio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda
{
    public sealed record DeleteAgendaResponse
    {
        public bool? Success { get; set; }
        public ErrorDto Error { get; set; }
    }
}
