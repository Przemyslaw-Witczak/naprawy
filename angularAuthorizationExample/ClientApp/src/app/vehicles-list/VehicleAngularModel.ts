export interface IVehicleAngularModel {
  displayErrorLine: string;
  id: number;
  brand: string;
  type: string;
  vin: string;
  engineNumber: string;
  registrationNumber: string;
  productionYear: number;
  purchaseDate: Date;  
  soldDate: Date | null;
  additionalInfo: string;
  engineCapacity: number;
  mileage: number;
  mileageDate: Date;
}

