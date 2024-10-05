using Newtonsoft.Json;
using ride_wise_api.Domain.Enums;

namespace ride_wise_api.Application.Models
{    
    public class DeliveryAgentRequest
    {        
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }        
        public DateTime Data_nascimento { get; set; }
        public int Numero_cnh { get; set; }
        public DriverLicenseType Tipo_cnh { get; set; }
        public string Image_cnh { get; set; }
    }
}
