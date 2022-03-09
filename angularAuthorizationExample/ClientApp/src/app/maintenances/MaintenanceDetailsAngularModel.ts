import { CzesciDictionaryAngularModel } from "../czesci-dictionary/CzesciDictionaryAngularModel";
import { CzynnosciDictionaryAngularModel } from "../maintenances-dictionary/CzynnosciDictionaryAngularModel";

export interface IMaintenanceDetailsAngularModel {
    deleted: boolean;    
    idMaintenanceDetails: number;
    idMaintenance: number;
    part: CzesciDictionaryAngularModel;    
    maintenance: CzynnosciDictionaryAngularModel;
    description: string;
    quantity: number;
    price: number;
    cost: number;
  }
  
  