namespace angularAuthorizationExample.Models
{
    public class VehicleModel
    {
        public int Id { get; set; } = 0;
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Vin { get; set; }
        public string EngineNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public short ProductionYear { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public string AdditionalInfo { get; set; }
        public short? EngineCapacity { get; set; }
        public int Mileage { get; set; }
        public DateTime? MileageDate { get; set; }

        public decimal TotalCost {get; set;}
        public int Distance {get; set;}
    }
}