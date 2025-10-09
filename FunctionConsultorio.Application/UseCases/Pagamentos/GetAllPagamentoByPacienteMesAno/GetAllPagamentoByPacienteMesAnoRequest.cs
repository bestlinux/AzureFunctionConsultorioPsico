using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamento;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamentoByPacienteMesAno
{
    public sealed record GetAllPagamentoByPacienteMesAnoRequest (int PacienteID, int Mes, int Ano) : IRequest<IReadOnlyCollection<GetAllPagamentoByPacienteMesAnoResponse>>;
}
