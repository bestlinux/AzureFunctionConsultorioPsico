using FunctionConsultorio.Application.UseCases.Agendas.CreateAgenda;
using FunctionConsultorio.Application.UseCases.Agendas.UpdateAgenda;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.Shared.Validator
{
    public class AgendaCreateValidator : AbstractValidator<CreateAgendaRequest>
    {
        public AgendaCreateValidator()
        {
            RuleFor(x => x.InicioSessao).NotEmpty();
            RuleFor(x => x.FimSessao).NotEmpty();
        }
    }
    public class AgendaUpdateValidator : AbstractValidator<UpdateAgendaRequest>
    {
        public AgendaUpdateValidator()
        {
            RuleFor(x => x.InicioSessao).NotEmpty();
            RuleFor(x => x.FimSessao).NotEmpty();
        }
    }
}
