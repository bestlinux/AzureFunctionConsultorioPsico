using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Pacientes.GetByIdPaciente
{
    public sealed record GetByIdPacienteRequest(int Id) : IRequest<GetByIdPacienteResponse> { };
}
