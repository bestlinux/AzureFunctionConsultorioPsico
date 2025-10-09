using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using FunctionConsultorio.Application.UseCases.Pagamentos.CreatePagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.UpdatePagamento;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.Shared.Validator
{
    public class PagamentoCreateValidator : AbstractValidator<CreatePagamentoRequest>
    {
        public PagamentoCreateValidator()
        {
            RuleFor(x => x.Ano).NotEmpty();
            RuleFor(x => x.StatusPagamento).NotEmpty();
            RuleFor(x => x.Mes).NotEmpty();
            RuleFor(x => x.Valor).NotEmpty();
        }
    }

    public class PagamentoUpdateValidator : AbstractValidator<UpdatePagamentoRequest>
    {
        public PagamentoUpdateValidator()
        {
            RuleFor(x => x.Ano).NotEmpty();
            RuleFor(x => x.StatusPagamento).NotEmpty();
            RuleFor(x => x.Mes).NotEmpty();
            RuleFor(x => x.Valor).NotEmpty();
        }
    }

}
