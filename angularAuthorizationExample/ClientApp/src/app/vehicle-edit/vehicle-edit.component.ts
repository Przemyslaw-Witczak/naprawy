import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IVehicleAngularModel } from '../vehicles-list/VehicleAngularModel';

@Component({
  selector: 'app-vehicle-edit',
  templateUrl: './vehicle-edit.component.html',
  styleUrls: ['./vehicle-edit.component.css']
})
export class VehicleEditComponent implements OnInit {  
  @Input() public vehicleId: any;
  submitted = false;
  public vehicle : IVehicleAngularModel | null = null;
  topicHasError = true;
  soldDateIsChecked = false;
  handleData: any;
  errorMsg = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) 
  {
    
  }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('vehicleId') || '');
    this.vehicleId = id;
    console.log('Inicjalizacja formularza edycji pojazdu='+this.vehicleId)
    if (this.vehicleId!=NaN)
    {
      this.http.get<IVehicleAngularModel>(this.baseUrl + 'Vehicles/'+this.vehicleId).subscribe(result => {
        this.vehicle = result;    
        if (this.vehicle.soldDate) 
          this.soldDateIsChecked = true;       
        console.log(this.vehicle);  
      }, error => console.error(error));
    }
    else
    {
      this.vehicle = <IVehicleAngularModel>{};
    }
    
    
  }

  onSoldDateClick()
  {
    this.soldDateIsChecked = !this.soldDateIsChecked;
    if (this.soldDateIsChecked && this.vehicle && !this.vehicle.soldDate)  
      this.vehicle.soldDate = new Date();

  }

  gotoVehiclesList()
  {
    this.router.navigate(['/vehicles-list']);
  }

  onSubmit()
  {
    this.submitted = true;
    if (!this.soldDateIsChecked && this.vehicle)
      this.vehicle.soldDate = null;
    console.log(this.vehicle);
    
    const headers = new HttpHeaders({ 'Content-Type': 'text/json', 'accept': '*/*' });
    const body = JSON.stringify(this.vehicle);
    console.log(headers);
    console.log(body);       
      
    this.http.post(this.baseUrl + 'Vehicles', body, {headers}).subscribe(result => {
          console.log('Vehicle data saved!', result);
          if (this.vehicle!=null)
            this.router.navigate(['/vehicles-list', this.vehicle.id]);    
        }, error => {
          console.error('Error!', error);
          this.errorMsg = error.error;
        });

      
      
        
  }



}
