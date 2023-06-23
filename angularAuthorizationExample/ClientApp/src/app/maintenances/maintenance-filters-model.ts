export interface MaintenanceFiltersModel {
  maintenanceDateFrom: Date | null;
  maintenanceDateTo: Date | null;
  partName: string;
  maintenanceName: string;
  sumFuelCosts: boolean;
  sumMaintenanceCosts: boolean;
}
