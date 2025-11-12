using AutoMapper;
using FunctionConsultorio.Application.Services.Notifications;
using FunctionConsultorio.Application.UseCases.Agendas.DeleteAgenda;
using FunctionConsultorio.Domain.Entities;
using FunctionConsultorio.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.DeleteAgendaPacienteByRecorrencia
{
    public class DeleteAgendaPacienteByRecorrenciaHandler : IRequestHandler<DeleteAgendaPacienteByRecorrenciaRequest, DeleteAgendaResponse>
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMediator _mediator;

        public DeleteAgendaPacienteByRecorrenciaHandler(IAgendaRepository agendaRepository,
        IMediator mediator)
        {
            _agendaRepository = agendaRepository;
            _mediator = mediator;
        }

        public async Task<DeleteAgendaResponse> Handle(DeleteAgendaPacienteByRecorrenciaRequest request, CancellationToken cancellationToken)
        {
            DeleteAgendaResponse deleteAgenteResponse = new();

            try
            {
                await _agendaRepository.DeletaTodosAgendamentosPorRecorrencia(request.PacienteID);

                deleteAgenteResponse.Success = true;

                return deleteAgenteResponse;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao excluir a agenda de id " + request.PacienteID,
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
                errorDto.Title = "Erro ao excluir a agenda " + request.PacienteID;
                deleteAgenteResponse.Error = errorDto;

                return deleteAgenteResponse;
            }
        }
    }
}
