import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';


@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {

  public vehiclesList: VehicleAngularModel[] = [];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<VehicleAngularModel[]>(baseUrl + 'Vehicles').subscribe(result => {
      this.vehiclesList = result;
    }, error => console.error(error));
    console.log("Adres do kontrollera: "+baseUrl);
  }

  ngOnInit(): void {
  }

}

interface VehicleAngularModel {
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
