using FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Pagamentos.DeletePagamento
{
    public sealed record DeletePagamentoRequest(int Id) : IRequest<bool> { };
}
