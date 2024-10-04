using Newtonsoft.Json;
using ride_wise_api.Domain.Enums;

namespace ride_wise_api.Application.Models
{    
    public class DeliveryAgentRequest
    {        
        public string? Identificador { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }        
        public string? DataNascimento { get; set; }
        public string? NumeroCnh { get; set; }
        public DriverLicenseType TipoCnh { get; set; }
        public string? ImageCnh { get; set; }
    }
}
