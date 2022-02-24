namespace angularAuthorizationExample.Models
{
    public class VehicleModel
    {
        public int Id { get; internal set; }
        public string Brand { get; internal set; }
        public string Type { get; internal set; }
        public string Vin { get; internal set; }
        public string EngineNumber { get; internal set; }
        public string RegistrationNumber { get; internal set; }
        public short ProductionYear { get; internal set; }
        public DateTime? PurchaseDate { get; internal set; }
        public DateTime? SoldDate { get; internal set; }
        public string AdditionalInfo { get; internal set; }
        public short? EngineCapacity { get; internal set; }
        public int Mileage { get; internal set; }
        public DateTime? MileageDate { get; internal set; }
    }
}