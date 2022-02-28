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

  public vehicle : IVehicleAngularModel | null = null;
  topicHasError = true;
  soldDateIsChecked = false;
  handleData: any;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) 
  {
    
  
  }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('vehicleId') || '');
    this.vehicleId = id;
    
    //let queryParams = new HttpParams();
    //queryParams = queryParams.append("vehicleId",this.vehicleId);

    //this.http.get<IVehicleAngularModel>(this.baseUrl + 'Vehicles/', {params: queryParams}).subscribe(result => {
    this.http.get<IVehicleAngularModel>(this.baseUrl + 'Vehicles/'+this.vehicleId).subscribe(result => {
      this.vehicle = result;      
    }, error => console.error(error));

  }

  onSoldDateClick()
  {
    this.soldDateIsChecked = !this.soldDateIsChecked;
    
    // if (this.soldDateIsChecked && this.vehicle?.soldDate==null)
    //   this.vehicle?.soldDate = Date.now();
  }

  gotoVehiclesList()
  {
    this.router.navigate(['/vehicles-list']);
  }

  onSubmit()
  {
    console.log(this.vehicle);
    // this.http.post<any>(this.baseUrl + 'Vehicles', this.vehicle).subscribe(result => {
    //   console.log('Success!', result)
    // }, error => console.error('Error!', error));
      const headers = new HttpHeaders({ 'Content-Type': 'text/json', 'accept': '*/*' });
      const body = JSON.stringify(this.vehicle);
      console.log(headers);
      console.log(body);
        return this.http.post(this.baseUrl + 'Vehicles', body, {headers}).subscribe(result => {
            console.log('Success!', result)
          }, error => console.error('Error!', error));
  }

}
