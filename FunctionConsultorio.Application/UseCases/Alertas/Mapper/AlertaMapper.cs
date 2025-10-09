using FunctionConsultorio.Application.UseCases.Alertas.GetAllAlertasByMes;
using FunctionConsultorio.Application.UseCases.Pagamentos.CreatePagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamento;
using FunctionConsultorio.Application.UseCases.Pagamentos.GetAllPagamentoByPacienteMesAno;
using FunctionConsultorio.Application.UseCases.Pagamentos.UpdatePagamento;
using FunctionConsultorio.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Alertas.Mapper
{
    public class AlertaMapper : Profile
    {
        public AlertaMapper()
        {
            CreateMap<GetAllAlertasByMesAnoRequest, Alerta>();
            CreateMap<Alerta, GetAllAlertasByMesAnoResponse>();
        }
    }
}
