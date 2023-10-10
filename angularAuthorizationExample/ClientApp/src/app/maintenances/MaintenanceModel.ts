import { IMaintenanceDetailsAngularModel } from "./MaintenanceDetailsAngularModel";

export interface IMaintenanceAngularModel {
    id: number;
    idVehicle: number;
    maintenanceDate: string;
    mileage: number;
    distance: number;
    cost: number;
    description: string;
    errorMessage: string;
    maintenanceDetailsList: IMaintenanceDetailsAngularModel[];
  }
  
