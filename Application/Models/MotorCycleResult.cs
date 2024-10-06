using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace ride_wise_api.Application.Models
{    
    public class MotorcycleResult
    {        
        public string Identificador { get; set; }        
        public int Ano { get; set; }        
        public string Modelo { get; set; }        
        public string Placa { get; set; }
    }
}
