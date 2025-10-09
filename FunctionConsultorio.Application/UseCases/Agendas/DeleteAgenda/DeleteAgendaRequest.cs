using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda
{
    public class DeleteAgendaRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
