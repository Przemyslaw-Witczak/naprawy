import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IMaintenanceDetailsAngularModel } from '../maintenances/MaintenanceDetailsAngularModel';

@Component({
  selector: 'app-maintenances-details',
  templateUrl: './maintenances-details.component.html',
  styleUrls: ['./maintenances-details.component.css']
})
export class MaintenancesDetailsComponent implements OnInit {
  @Input('parentMaintenanceId') public parentMaintenanceId : number = 0;
  

  public detailsList: IMaintenanceDetailsAngularModel[] = [];
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) { 
    
  }

  ngOnInit(): void {

    // let id = parseInt(this.route.snapshot.paramMap.get('maintenanceId') || '');
    // this.parentMaintenanceId = id;
    this.http.get<IMaintenanceDetailsAngularModel[]>(this.baseUrl + 'MaintenancesDetails/'+this.parentMaintenanceId).subscribe(result => {
      this.detailsList = result;      
    }, error => console.error(error));

  }

}
