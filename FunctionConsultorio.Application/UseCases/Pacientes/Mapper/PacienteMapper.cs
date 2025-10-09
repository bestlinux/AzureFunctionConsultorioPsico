using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetByIdPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
using FunctionConsultorio.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Pacientes.Mapper
{
    public sealed class PacienteMapper : Profile
    {
        public PacienteMapper() 
        {
            CreateMap<CreatePacienteRequest, Paciente>();
            CreateMap<Paciente, CreatePacienteResponse>();
            CreateMap<UpdatePacienteRequest, Paciente>();
            CreateMap<Paciente, UpdatePacienteResponse>();
            CreateMap<GetAllPacienteRequest, Paciente>();
            CreateMap<Paciente, GetAllPacienteResponse>();
            CreateMap<GetByIdPacienteRequest, Paciente>();
            CreateMap<Paciente, GetByIdPacienteResponse>();
        }
    }
}
