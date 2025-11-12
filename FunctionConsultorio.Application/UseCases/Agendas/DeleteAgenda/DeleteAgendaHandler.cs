using AutoMapper;
using FunctionConsultorio.Application.Services.Notifications;
using FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente;
using FunctionConsultorio.Domain.Entities;
using FunctionConsultorio.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda
{
    public class DeleteAgendaHandler : IRequestHandler<DeleteAgendaRequest, DeleteAgendaResponse>
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

        public async Task<DeleteAgendaResponse> Handle(DeleteAgendaRequest request, CancellationToken cancellationToken)
        {
            DeleteAgendaResponse deleteAgenteResponse = new();

            try
            {
                await _agendaRepository.RemoveAsync(request.Id);

                deleteAgenteResponse.Success = true;

                return deleteAgenteResponse;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao excluir a agenda de id " + request.Id,
                    Stack = ex.StackTrace,
                }, cancellationToken);
                deleteAgenteResponse.Success = false;
                ErrorDto errorDto = new();
                List<ErrorItem> errorsList = new();
                ErrorItem errorItem = new()
                {
                    Message = ex.Message
                };
                errorsList.Add(errorItem);
                errorDto.Errors = errorsList;
                errorDto.Title = "Erro ao excluir a agenda " + request.Id;
                deleteAgenteResponse.Error = errorDto;

                return deleteAgenteResponse;
            }
        }
    }
}
