using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Application.UseCases.Prontuarios.CreateProntuario
{
    public class CreateProntuarioResponse
    {
        public int? PacienteId { get; set; }

        public string? Pagina { get; set; }

        public string? Conteudo { get; set; }
    }
}
