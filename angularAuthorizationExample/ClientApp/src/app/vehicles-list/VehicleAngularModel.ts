export interface IVehicleAngularModel {
  id: number;
  brand: string;
  type: string;
  vin: string;
  engineNumber: string;
  registrationNumber: string;
  productionYear: number;
  purchaseDate: Date;
  soldDate: Date;
  additionalInfo: string;
  engineCapacity: number;
  mileage: number;
  mileageDate: Date;
}
