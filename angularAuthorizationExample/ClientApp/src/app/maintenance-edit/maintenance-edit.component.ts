import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NumberValueAccessor, FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { IMaintenanceDetailsAngularModel } from '../maintenances/MaintenanceDetailsAngularModel';
import { IMaintenanceAngularModel } from '../maintenances/MaintenanceModel';
import { IVehicleAngularModel } from '../vehicles-list/VehicleAngularModel';

@Component({
  selector: 'app-maintenance-edit',
  templateUrl: './maintenance-edit.component.html',
  styleUrls: ['./maintenance-edit.component.css']
})
export class MaintenanceEditComponent implements OnInit {

  public vehicleId: number = 0;
  public maintenanceId: number | null=0;
  public maintenance: IMaintenanceAngularModel | null = null;
  
  public errorMsg = '';
  public debugData = '';
  public submitted: boolean = false;
  public hideMaintenance: boolean = false;
  public changedDetails: boolean = false;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) {
    
    let vehicleId = parseInt(this.route.snapshot.paramMap.get('vehicleId') || '');
    this.vehicleId = vehicleId;
    let id = parseInt(this.route.snapshot.paramMap.get('maintenanceId') || '',10);
    // console.log(this.route.snapshot.paramMap.get('maintenanceId'));
    this.maintenanceId = id;
    console.log(`Inicjalizacjaaa formularza edycji naprawy=${this.maintenanceId} vehicleId=${this.vehicleId}`)
    if (this.maintenanceId!=null && this.maintenanceId>0)
    {
      this.http.get<IMaintenanceAngularModel>(this.baseUrl + 'MaintenancesEdit/'+this.maintenanceId).subscribe(result => {
        this.maintenance = result;    
      }, error => this.errorMsg = error);
    }
    else
    {
      const currentDate = new Date();

      // Format the date as "yyyy-MM-dd"
      const year = currentDate.getFullYear();
      const month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Adding 1 because months are 0-based
      const day = String(currentDate.getDate()).padStart(2, '0');

      // Combine the parts into the desired format
      // let maintenanceDate = `${year}-${month}-${day}`;
      this.maintenance = <IMaintenanceAngularModel>{ idVehicle: this.vehicleId, maintenanceDate: `${year}-${month}-${day}` };
    }


   }

  ngOnInit(): void {
    
  }

  onSubmit()
  {
        
    const headers = new HttpHeaders({ 'Content-Type': 'text/json', 'accept': '*/*' });
    const body = JSON.stringify(this.maintenance);
    console.log(headers);
    console.log(body);       
      
    this.http.post(this.baseUrl + 'Maintenances', body, {headers}).subscribe(result => {
          console.log('Maintenance data saved!', result);
          this.goBack();
        }, error => {
          console.error('Error!', error);
          this.errorMsg = error.message;
          this.debugData = body;
        });

    this.submitted = true;
    this.maintenance = null;
    console.log(this.maintenance);
  }

  goBack()
  {
    console.log("Navigate back to: Maintenances: VehicleId="+this.vehicleId+" MaintenanceId="+this.maintenanceId);
    this.router.navigate(['/maintenances', this.vehicleId, this.maintenanceId]);
  }

  onPositionsChanged(savedPositions: IMaintenanceDetailsAngularModel[])
  {
    if (savedPositions)
    {
      this.changedDetails = true;
      if (this.maintenance) {
        this.maintenance.maintenanceDetailsList = savedPositions;
        console.log(savedPositions);
      }
        
    }
  }
}
