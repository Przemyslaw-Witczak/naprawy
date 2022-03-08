import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NumberValueAccessor } from '@angular/forms';
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
  public maintenanceId: number | null | undefined;
  public maintenance: IMaintenanceAngularModel | null = null;
  public errorMsg = '';
  public submitted: boolean = false;
  public hideMaintenance: boolean = false;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let vehicleId = parseInt(this.route.snapshot.paramMap.get('vehicleId') || '');
    this.vehicleId = vehicleId;
    let id = parseInt(this.route.snapshot.paramMap.get('maintenanceId') || '');
    this.maintenanceId = id;
    console.log('Inicjalizacja formularza edycji naprawy='+this.maintenanceId)
    if (this.maintenanceId!=NaN)
    {
      this.http.get<IMaintenanceAngularModel>(this.baseUrl + 'MaintenancesEdit/'+this.maintenanceId).subscribe(result => {
        this.maintenance = result;    

      }, error => this.errorMsg = error);
    }
    else
    {
      this.maintenance = <IMaintenanceAngularModel>{};
    }
  }

  onSubmit()
  {
    this.submitted = true;
    
    console.log(this.maintenance);
    
    const headers = new HttpHeaders({ 'Content-Type': 'text/json', 'accept': '*/*' });
    const body = JSON.stringify(this.maintenance);
    console.log(headers);
    console.log(body);       
      
    this.http.post(this.baseUrl + 'MaintenancesEdit', body, {headers}).subscribe(result => {
          console.log('Maintenance data saved!', result);
          if (this.maintenance!=null)
            this.goBack();
        }, error => {
          console.error('Error!', error);
          this.errorMsg = error.error;
        });
  }

  goBack()
  {
    console.log("Navigate back to: Maintenances: VehicleId="+this.vehicleId+" MaintenanceId="+this.maintenanceId);
    this.router.navigate(['/maintenances', this.vehicleId, this.maintenanceId]);
  }
}
