using FunctionConsultorio.Application.UseCases.Pagamentos.CreatePagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamentoByPacienteMesAno;
using FunctionConsultorio.Application.UseCases.Pagamentos.UpdatePagamento;
using FunctionConsultorio.Application.UseCases.Prontuarios.GetAllProntuario;
using FunctionConsultorio.Application.UseCases.Prontuarios.GetAllProntuarioByPaciente;
using FunctionConsultorio.Application.UseCases.Prontuarios.UpdateProntuario;
using FunctionConsultorio.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Prontuarios.Mapper
{
    public class ProntuarioMapper : Profile
    {
        public ProntuarioMapper()
        {
            CreateMap<GetAllProntuarioRequest, Prontuario>();
            CreateMap<Prontuario, GetAllProntuarioResponse>();
            CreateMap<GetAllProntuarioByPacienteRequest, Prontuario>();
            CreateMap<Prontuario, GetAllProntuarioByPacienteResponse>();
            CreateMap<UpdateProntuarioRequest, Prontuario>();
            CreateMap<Prontuario, UpdateProntuarioResponse>();
        }
    }
}
