using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using RideWise.Api.Domain.Enums;

namespace RideWise.Api.Application.Models
{
    [Serializable]
    public class DeliveryAgentResult
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime Data_nascimento { get; set; }
        public int Numero_cnh { get; set; }        
        public DriverLicenseType Tipo_cnh { get; set; }
    }
}
