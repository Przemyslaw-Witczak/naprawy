namespace angularAuthorizationExample.Models
{
    public class MaintenanceModel
    {
        public int IdVehicle {get; set;}
        public int Id {get; set;}
        //ToDo: Change it to string type
        public string MaintenanceDate{get; set;}    
        public Decimal Mileage{get; set;}
        public string? Description{get; set;}
        public decimal Cost{get; set;}
        public int Distance { get; set; }
        public List<MaintenanceDetailsModel>?MaintenanceDetailsList{get; set;}
    }
}