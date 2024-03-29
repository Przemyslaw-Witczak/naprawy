namespace angularAuthorizationExample.Models
{
    public class MaintenanceDetailsModel
    {
        public int IdMaintenanceDetails{get; set;}
        public int IdMaintenance{get; set;}

        public DictionaryItem? Part{get; set;}
        public DictionaryItem? Maintenance{get; set;}

        // public int? IdPartDictionary{get; set;}
        // public string? PartName {get;set;}
        // public int? IdMaintenanceDictionary{get; set;}
        // public string? MaintenanceName {get;set;}
        public string? Description{get; set;}
        public decimal Quantity{get; set;}
        public decimal Price{get; set;}
        public decimal Cost{get; set;}
        public bool Deleted{get; set;}
    }
}