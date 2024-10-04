using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace ride_wise_api.Application.Models
{
    [Serializable]
    public class MotorcycleResult
    {        
        public string Identificador { get; set; }        
        public int Year { get; set; }        
        public string Model { get; set; }        
        public string LicensePlate { get; set; }
    }
}
