namespace RideWise.Api.Application.Models
{
    public class RentalResult
    {
        public string Entregador_id { get; set; }
        public string Moto_id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_termino { get; set; }
        public DateTime Data_previsao_termino { get; set; }
        public string Plano { get; set; }
        public decimal Valor_diaria { get; set; }
    }
}
