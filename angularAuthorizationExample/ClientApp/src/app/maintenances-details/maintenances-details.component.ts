import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IMaintenanceDetailsAngularModel } from '../maintenances/MaintenanceDetailsAngularModel';

@Component({
  selector: 'app-maintenances-details',
  templateUrl: './maintenances-details.component.html',
  styleUrls: ['./maintenances-details.component.css']
})
export class MaintenancesDetailsComponent implements OnInit {
  vehicleId:number;
  public detailsList: IMaintenanceDetailsAngularModel[] = [];
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) { 
    let id = parseInt(this.route.snapshot.paramMap.get('maintenanceId') || '');
    this.vehicleId = id;
    http.get<IMaintenanceDetailsAngularModel[]>(baseUrl + 'MaintenancesDetails/'+this.vehicleId).subscribe(result => {
      this.detailsList = result;      
    }, error => console.error(error));
  }

  ngOnInit(): void {
  }

}
