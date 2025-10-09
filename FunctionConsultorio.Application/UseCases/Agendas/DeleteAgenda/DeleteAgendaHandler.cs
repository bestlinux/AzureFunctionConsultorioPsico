using FunctionConsultorio.Application.Services.Notifications;
using FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente;
using FunctionConsultorio.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda
{
    public class DeleteAgendaHandler : IRequestHandler<DeleteAgendaRequest, bool>
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMediator _mediator;

        public DeleteAgendaHandler(IAgendaRepository agendaRepository,
        IMapper mapper,
        IMediator mediator)
        {
            _agendaRepository = agendaRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteAgendaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _agendaRepository.RemoveAsync(request.Id);

                return true;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao excluir a agenda de id " + request.Id,
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return false;
            }
        }
    }
}
