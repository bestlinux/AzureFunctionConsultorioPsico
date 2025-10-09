using FunctionConsultorio.Application.UseCases.Agendas.CreateAgenda;
using FunctionConsultorio.Application.UseCases.Agendas.GetAllAgenda;
using FunctionConsultorio.Application.UseCases.Agendas.UpdateAgenda;
using FunctionConsultorio.Application.UseCases.Pagamentos.CreatePagamento;
using FunctionConsultorio.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Agendas.Mapper
{
    public class AgendaMapper : Profile
    {
        public AgendaMapper()
        {
            CreateMap<CreateAgendaRequest, Agenda>();
            CreateMap<Agenda, CreateAgendaResponse>();
            CreateMap<GetAllAgendaRequest, Agenda>();
            CreateMap<Agenda, GetAllAgendaResponse>();
            CreateMap<UpdateAgendaRequest, Agenda>();
            CreateMap<Agenda, UpdateAgendaResponse>();
        }
    }
}
