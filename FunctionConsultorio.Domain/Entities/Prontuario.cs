using FunctionConsultorio.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Domain.Entities
{
    public class Prontuario : Entity
    {
        public Paciente? Paciente { get; set; }

        public int? PacienteId { get; set; }

        public string? Pagina { get; set; }

        [Column(TypeName = "ntext")]
        public string? Conteudo { get; set; }

    }
}
