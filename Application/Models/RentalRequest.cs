namespace ride_wise_api.Application.Models
{
    public class RentalRequest
    {
        public string Entregador_id { get; set; }
        public string Moto_id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_termino { get; set; }
        public DateTime Data_previsao_termino { get; set; }        
        public int Plano { get; set; }                
    }
}
