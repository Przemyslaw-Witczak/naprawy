namespace angularAuthorizationExample.Models
{
    public class VehicleModel
    {
        public int Id { get; set; } = 0;
        public string Brand { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Vin { get; set; } = string.Empty;
        public string EngineNumber { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public short ProductionYear { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public short? EngineCapacity { get; set; }
        public int? Mileage { get; set; }
        public DateTime? MileageDate { get; set; }

        public decimal? TotalCost {get; set;}
        public int? Distance {get; set;}

        public override string ToString()
        {
            return $"{Id} {Brand} {Type} {RegistrationNumber}";
        }
    }
}