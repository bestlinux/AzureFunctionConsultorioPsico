using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Pacientes.DeletePaciente
{
    public sealed record DeletePacienteRequest(int Id) : IRequest<DeletePacienteResponse> { };
}
