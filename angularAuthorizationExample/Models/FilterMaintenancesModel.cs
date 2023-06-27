namespace angularAuthorizationExample.Models
{
    public class FilterMaintenancesModel
    {
        public int VehicleId { get; set; }        

        public Nullable<DateTime> MaintenanceDateFrom { get; set; }
    public Nullable<DateTime> MaintenanceDateTo { get; set; }
        public string PartName { get; set; }
        public string MaintenanceName { get; set; }
        public bool SumFuelCosts { get; set; }
        public bool SumMaintenanceCosts { get; set; }
    }
}
