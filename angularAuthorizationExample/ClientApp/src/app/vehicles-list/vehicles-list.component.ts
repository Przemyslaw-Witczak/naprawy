import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { IVehicleAngularModel } from './VehicleAngularModel';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {

  @Input() public editedVehicleId: any;
  public vehiclesList: IVehicleAngularModel[] = [];
  public selectedVehicle: IVehicleAngularModel | null = null;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router, private route: ActivatedRoute) {
    http.get<IVehicleAngularModel[]>(baseUrl + 'Vehicles').subscribe(result => {
      this.vehiclesList = result;
      this.selectedVehicle = this.selectVehicleById(this.editedVehicleId);
    }, error => console.error(error));
    console.log("Adres do kontrollera: "+baseUrl);
    console.log("Pobrano "+this.vehiclesList.length+' pojazdów do wyświetlenia.');
    
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

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('vehicleId') || '');
    this.editedVehicleId = id;
    console.log("Input selected id:"+id);
    
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
    this.router.navigate(['/vehicle-edit', vehicle.id])
  }

  onCreate()
  {
    this.router.navigate(['/vehicle-edit', 0]);
  }
}


