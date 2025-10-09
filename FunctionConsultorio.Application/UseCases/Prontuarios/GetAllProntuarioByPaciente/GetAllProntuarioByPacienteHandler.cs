using FunctionConsultorio.Application.Services.Notifications;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamentoByPacienteMesAno;
using FunctionConsultorio.Domain.Entities;
using FunctionConsultorio.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Prontuarios.GetAllProntuarioByPaciente
{
    public class GetAllProntuarioByPacienteHandler : IRequestHandler<GetAllProntuarioByPacienteRequest, IReadOnlyCollection<GetAllProntuarioByPacienteResponse>>
    {
        private readonly IProntuarioRepository _prontuarioRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAllProntuarioByPacienteHandler(IProntuarioRepository prontuarioRepository,
        IMapper mapper,
        IMediator mediator)
        {
            _prontuarioRepository = prontuarioRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IReadOnlyCollection<GetAllProntuarioByPacienteResponse>> Handle(GetAllProntuarioByPacienteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Prontuario>? enumerable = null;
                var prontuarios = enumerable;

                prontuarios = await _prontuarioRepository.LocalizaTodosProntuariosComPaciente(request.PacienteID);

                return prontuarios.Select(_mapper.Map<GetAllProntuarioByPacienteResponse>).ToList();
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao buscar prontuarios por paciente",
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }
        }
    }
}
