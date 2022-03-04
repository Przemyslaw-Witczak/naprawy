namespace angularAuthorizationExample.Models
{
    public class MaintenanceDetailsModel
    {
        public int IdMaintenanceDetails{get; set;}
        public int IdMaintenance{get; set;}
        public int? IdPartDictionary{get; set;}
        public string? PartName {get;set;}
        public int? IdMaintenanceDictionary{get; set;}
        public string? MaintenanceName {get;set;}
        public string? Description{get; set;}
        public int Quantity{get; set;}
        public decimal Price{get; set;}
        public decimal Cost{get; set;}
    }
}