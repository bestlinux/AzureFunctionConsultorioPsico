using FunctionConsultorio.Application.UseCases.Alertas.GetAllAlertasByMes;
using FunctionConsultorio.Application.UseCases.Mail.SendEmailAgenda;
using FunctionConsultorio.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Mail.Mapper
{
    public class EmailMapper : Profile
    {
        public EmailMapper() 
        {
            CreateMap<SendEmailAgendaRequest, EmailAgenda>();
        }
    }
}
