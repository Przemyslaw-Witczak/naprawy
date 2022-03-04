import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IVehicleAngularModel } from '../vehicles-list/VehicleAngularModel';
import { IMaintenanceAngularModel } from './MaintenanceModel';

@Component({
  selector: 'app-maintenances',
  templateUrl: './maintenances.component.html',
  styleUrls: ['./maintenances.component.css']
})
export class MaintenancesComponent implements OnInit {
  public selectedVehicle: IVehicleAngularModel | null | undefined = null;
  public vehiclesList: IVehicleAngularModel[] = [];
  public maintenancesList: IMaintenanceAngularModel[] = []

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) { 
    http.get<IVehicleAngularModel[]>(baseUrl + 'Vehicles').subscribe(result => {
      this.vehiclesList = result;      
    }, error => console.error(error));
  }

  ngOnInit(): void {

  }

  selectVehicle(){
    this.selectedVehicle = undefined;
  }

  onVehicleSelect(vehicle: IVehicleAngularModel)
  {
    this.selectedVehicle = vehicle;
    
    this.http.get<IMaintenanceAngularModel[]>(this.baseUrl + 'Maintenances/'+vehicle.id).subscribe(result => {
        this.maintenancesList = result;      
      }, error => console.error(error));
  }

  onMaintenanceSelect(maintenance: IMaintenanceAngularModel)
  {
    //show details of maintenances
    this.router.navigate([maintenance.id], {relativeTo: this.route});
  }

  isMaintenanceSelected(maintenance: IMaintenanceAngularModel)
  {
    //check if maintenance is selected
  }

  onMaintenanceEdit(maintenance: IMaintenanceAngularModel)
  {
    //on edit maintenance click
  }

  onMaintenanceDelete(maintenance: IMaintenanceAngularModel)
  {
    //on maintenance delete
  }
}
