import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { IVehicleAngularModel } from './VehicleAngularModel';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';


@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {

  public vehiclesList: IVehicleAngularModel[] = [];
  public selectedVehicle: IVehicleAngularModel | null = null;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router, private route: ActivatedRoute) {
    http.get<IVehicleAngularModel[]>(baseUrl + 'Vehicles').subscribe(result => {
      this.vehiclesList = result;
      this.selectedVehicle = this.vehiclesList[0];
    }, error => console.error(error));
    console.log("Adres do kontrollera: "+baseUrl);
  }

  ngOnInit(): void {
  }

  onSelect(vehicle: IVehicleAngularModel | null)
  {
    this.selectedVehicle = vehicle;
  }

  isSelected(vehicle: IVehicleAngularModel | null)
  {
    return (this.selectedVehicle!=null && this.selectedVehicle.id == vehicle?.id)
  }

  onEdit(vehicle:IVehicleAngularModel)
  {
    this.router.navigate(['/vehicles-list', vehicle.id])
  }
}


