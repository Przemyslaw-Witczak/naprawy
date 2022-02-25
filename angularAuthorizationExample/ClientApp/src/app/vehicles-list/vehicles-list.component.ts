import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { IVehicleAngularModel } from './VehicleAngularModel';


@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {

  public vehiclesList: IVehicleAngularModel[] = [];
  public selectedVehicle: IVehicleAngularModel | null = null;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IVehicleAngularModel[]>(baseUrl + 'Vehicles').subscribe(result => {
      this.vehiclesList = result;
      this.selectedVehicle = this.vehiclesList[0];
    }, error => console.error(error));
    console.log("Adres do kontrollera: "+baseUrl);
  }

  ngOnInit(): void {
  }

}


