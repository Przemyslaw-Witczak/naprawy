import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { IVehicleAngularModel } from './VehicleAngularModel';
import { Router, ActivatedRoute } from '@angular/router';
import { MsalService } from '@azure/msal-angular';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {
  public loadingVehicles: boolean=true;
  @Input() public editedVehicleId: any;
  public vehiclesList: IVehicleAngularModel[] = [];
  public selectedVehicle: IVehicleAngularModel | null = null;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute, private authService: MsalService) {

    const activeAccount = this.authService.instance.getActiveAccount();
    if (activeAccount) {
      this.authService.instance.acquireTokenSilent({
        account: activeAccount,
        scopes: ['user.read'],
      }).then(response => {
        //Declare headers as object type with Bearer token.
        const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + response.accessToken,
        });
        console.log(headers.keys().map(key => `${key}: ${headers.get(key)}`).join('\n'));
        http.get<IVehicleAngularModel[]>(baseUrl + 'Vehicles', { headers: headers }).subscribe(result => {
          this.vehiclesList = result;
          this.selectedVehicle = this.selectVehicleById(this.editedVehicleId);
          this.loadingVehicles = false;
        }, error => console.error(error));
      });
    }

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
//Another way to show confirmation window:
//https://stackoverflow.com/questions/41684114/easy-way-to-make-a-confirmation-dialog-in-angular
  onDelete(vehicle: IVehicleAngularModel)
  {
    if(confirm("Czy na pewno chcesz usunąć pojazd "+vehicle.brand+" "+vehicle.type+" "+vehicle.registrationNumber+ "?")) {

      const headers = new HttpHeaders({ 'Content-Type': 'text/json', 'accept': '*/*' });      
      console.log(headers);      
        
      this.http.delete(this.baseUrl + 'Vehicles/'+vehicle.id).subscribe(result => {
            console.log('Vehicle deleted!', result);
            console.log("Deleting vehicle id"+vehicle.id);
            //delete this.vehiclesList[this.vehiclesList.indexOf(vehicle)]; //this deletes element and sets it to undefined
            this.vehiclesList.splice(this.vehiclesList.indexOf(vehicle),1);
          }, error => {
            console.error('Error!', error);            
            vehicle.displayErrorLine = error.error.komunikat.errors[0].errorMessage;
            //this.errorMsg = error.message+error.error;
          });


      

  }
}
}
