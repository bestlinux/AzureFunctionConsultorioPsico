using FunctionConsultorio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Prontuarios.GetAllProntuarioByPaciente
{
    public class GetAllProntuarioByPacienteResponse
    {
        public int Id { get; set; }
        public Paciente? Paciente { get; set; }

        public int? PacienteId { get; set; }

        public string? Pagina { get; set; }

        public string? Conteudo { get; set; }
    }
}
