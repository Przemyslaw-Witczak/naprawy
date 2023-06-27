export interface MaintenanceFiltersModel {
  vehicleId: number;
  maintenanceDateFrom: Date | null;
  maintenanceDateTo: Date | null;
  partName: string;
  maintenanceName: string;
  sumFuelCosts: boolean;
  sumMaintenanceCosts: boolean;
}
