namespace angularAuthorizationExample.Models
{
    public class MaintenanceModel
    {
        public int Id {get; set;}
        public DateTime MaintenanceDate{get; set;}    
        public int Mileage{get; set;}
        public string? Description{get; set;}
        public decimal Cost{get; set;}
        public int Distance { get; set; }
    }
}