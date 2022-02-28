namespace angularAuthorizationExample.Models
{
    public class VehicleModel
    {
        public int Id { get; internal set; } = 0;
        public string brand { get; set; }
        public string type { get; set; }
        public string vin { get; set; }
        public string engineNumber { get; set; }
        public string registrationNumber { get; set; }
        public short productionYear { get; set; }
        public DateTime? purchaseDate { get; set; }
        public DateTime? soldDate { get; set; }
        public string additionalInfo { get; set; }
        public short? engineCapacity { get; set; }
        public int mileage { get; set; }
        public DateTime? mileageDate { get; set; }
    }
}