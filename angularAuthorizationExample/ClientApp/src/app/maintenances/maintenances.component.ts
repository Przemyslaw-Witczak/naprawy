import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IVehicleAngularModel } from '../vehicles-list/VehicleAngularModel';
import { MaintenanceFiltersModel } from './maintenance-filters-model';
import { IMaintenanceAngularModel } from './MaintenanceModel';

@Component({
  selector: 'app-maintenances',
  templateUrl: './maintenances.component.html',
  styleUrls: ['./maintenances.component.css']
})
export class MaintenancesComponent implements OnInit {
  inputVehicleId: number | undefined;
  public selectedVehicle: IVehicleAngularModel | null | undefined = null;
  public vehiclesList: IVehicleAngularModel[] = [];
  maintenanceFilters: MaintenanceFiltersModel | null = null;
  inputMaintenanceId: number | undefined;
  public maintenancesList: IMaintenanceAngularModel[] = []
  public selectedMaintenance: IMaintenanceAngularModel | null | undefined = null;

  filtersEnabled: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) { 
    http.get<IVehicleAngularModel[]>(baseUrl + 'Vehicles').subscribe(result => {
      this.vehiclesList = result;     
      if (this.inputVehicleId)
      {
        this.selectedVehicle = this.selectVehicleById(this.inputVehicleId); 
        if (this.selectedVehicle)
          this.onVehicleSelect(this.selectedVehicle);
      }
    }, error => console.error(error));
  }

  ngOnInit(): void {
    let vehicleId = parseInt(this.route.snapshot.paramMap.get('vehicleId') || '');
    if (vehicleId)
    {
      console.log("Select vehicle:"+vehicleId);
      this.inputVehicleId = vehicleId;
    }
      
    let maintenanceId = parseInt(this.route.snapshot.paramMap.get('maintenanceId') || '');
    if (maintenanceId)
    {
      console.log("Select maintenance:"+maintenanceId);
      this.inputMaintenanceId = maintenanceId;
    }    
  }

  selectVehicleById(vehicleId: number): IVehicleAngularModel | null
  {
    console.log("Selecting vehicle "+vehicleId+", from "+this.vehiclesList.length);
    let foundVehicle = null;
    this.vehiclesList.forEach(obj=>{
      if (obj.id==vehicleId)
        foundVehicle = obj;
      }
     )
      return foundVehicle;
  }

  selectVehicle(){
    this.selectedVehicle = undefined;
    this.selectedMaintenance = undefined;
  }

  onVehicleSelect(vehicle: IVehicleAngularModel)
  {
    this.selectedVehicle = vehicle;
    
    this.http.get<IMaintenanceAngularModel[]>(this.baseUrl + 'Maintenances/'+vehicle.id).subscribe(result => {
        this.maintenancesList = result;   
        if (this.inputMaintenanceId)   
        this.selectedMaintenance = this.selectMaintenanceById(this.inputMaintenanceId);
      }, error => console.error(error));
  }

  selectMaintenanceById(maintenanceId: number): IMaintenanceAngularModel | null
  {
    console.log("Selecting maintenance "+maintenanceId+", from "+this.maintenancesList.length);
    let foundVehicle = null;
    this.maintenancesList.forEach(obj=>{
      if (obj.id==maintenanceId)
        foundVehicle = obj;
      }
     )
      return foundVehicle;
  }

  onMaintenanceSelect(maintenance: IMaintenanceAngularModel)
  {
    //show details of maintenances
    //this.router.navigate([maintenance.id], {relativeTo: this.route});
    this.selectedMaintenance = maintenance;
  }

  onEditMaintenance(maintenance: IMaintenanceAngularModel)
  {
    this.router.navigate(['/maintenance-edit', this.selectedVehicle?.id, maintenance.id])
  }

  onAddMaintenance()
  {
    this.router.navigate(['/maintenance-edit', this.selectedVehicle?.id, 0]);
  }

  showHideFilters() {
    this.filtersEnabled = !this.filtersEnabled;
    if (this.filtersEnabled)
      this.maintenanceFilters = { maintenanceDateFrom: null, maintenanceDateTo: null, sumFuelCosts: true, sumMaintenanceCosts: true, partName: '', maintenanceName:'' };
  }

  getSummaryCost(): number
  {
    let summaryCost = 0;
    this.maintenancesList.forEach(obj=>{
      summaryCost = summaryCost + obj.cost;
      }
     )
      return summaryCost;
  }

  unselectMaintenance()
  {
    this.selectedMaintenance = null;
  }

  isMaintenanceSelected(maintenance: IMaintenanceAngularModel)
  {
    //check if maintenance is selected
  } 

  onMaintenanceDelete(maintenance: IMaintenanceAngularModel)
  {
    if(confirm("Czy na pewno chcesz usunąć pozycję z "+maintenance.maintenanceDate+ "?")) {

      const headers = new HttpHeaders({ 'Content-Type': 'text/json', 'accept': '*/*' });      
      console.log(headers);      
        
      this.http.delete(this.baseUrl + 'Maintenances/'+maintenance.id).subscribe(result => {
            console.log('Maintenance deleted!', result);
            console.log("Maintenance id"+maintenance.id);
            //delete this.vehiclesList[this.vehiclesList.indexOf(vehicle)]; //this deletes element and sets it to undefined
            this.maintenancesList.splice(this.maintenancesList.indexOf(maintenance),1);
            this.unselectMaintenance();
          }, error => {
            console.error('Error!', error);
            
            maintenance.errorMessage = error.error.komunikat.errors[0].errorMessage;            
          });
    }
  }
}
