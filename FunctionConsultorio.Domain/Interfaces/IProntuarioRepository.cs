using FunctionConsultorio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Domain.Interfaces
{
    public interface IProntuarioRepository : IRepository<Prontuario>
    {
        Task<IEnumerable<Prontuario>> LocalizaTodosProntuariosComPaciente(int PacienteId);
    }
}
