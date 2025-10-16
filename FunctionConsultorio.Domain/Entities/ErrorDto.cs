using System.Text.Json.Serialization;

namespace FunctionConsultorio.Domain.Entities
{
    public class ErrorDto
    {
        public string Title { get; set; }

        public List<ErrorItem> Errors { get; set; } = new();
    }
}
