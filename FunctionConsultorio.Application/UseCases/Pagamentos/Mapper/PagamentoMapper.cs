using FunctionConsultorio.Application.UseCases.Pacientes.CreatePaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetAllPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.GetByIdPaciente;
using FunctionConsultorio.Application.UseCases.Pacientes.UpdatePaciente;
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

namespace FunctionConsultorio.Application.UseCases.Pagamentos.Mapper
{
    public class PagamentoMapper : Profile
    {
        public PagamentoMapper()
        {
            CreateMap<CreatePagamentoRequest, Pagamento>();
            CreateMap<Pagamento, CreatePagamentoResponse>();
            CreateMap<UpdatePagamentoRequest, Pagamento>();
            CreateMap<Pagamento, UpdatePagamentoResponse>();
            CreateMap<GetAllPagamentoRequest, Pagamento>();
            CreateMap<Pagamento, GetAllPagamentoResponse>();
            CreateMap<GetAllPagamentoByPacienteMesAnoRequest, Pagamento>();
            CreateMap<Pagamento, GetAllPagamentoByPacienteMesAnoResponse>();
        }
    }
}
