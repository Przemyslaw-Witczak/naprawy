import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  @Input('editingMode') public editingMode : boolean = false;
  public madeChanges: boolean = false;
  public selectedMaintenancePosition: IMaintenanceDetailsAngularModel | null = null;
  public detailsList: IMaintenanceDetailsAngularModel[] = [];
  public deletedDetailsList: IMaintenanceDetailsAngularModel[] = [];
  
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) {     
  }

  ngOnInit(): void {    
    this.http.get<IMaintenanceDetailsAngularModel[]>(this.baseUrl + 'MaintenancesDetails/'+this.parentMaintenanceId).subscribe(result => {
      this.detailsList = result;  
      this.deletedDetailsList = [];
      this.madeChanges = false;
    }, error => console.error(error));

  }

  onMaintenanceCreate()
  {
    this.selectedMaintenancePosition = <IMaintenanceDetailsAngularModel>{
      quantity: 1.0,
      idMaintenance: this.parentMaintenanceId
    }
    this.detailsList.push(this.selectedMaintenancePosition);
    this.madeChanges = true;
  }

  onMaintenanceDelete(maintenanceDetail: IMaintenanceDetailsAngularModel)
  {
    if(confirm(`Czy na pewno chcesz usunąć pozycję ${maintenanceDetail.maintenance?.name} ${maintenanceDetail.part?.name} ${maintenanceDetail.description}?`)) {
      maintenanceDetail.deleted = true;            
      this.detailsList.splice(this.detailsList.indexOf(maintenanceDetail),1);
      this.deletedDetailsList.push(maintenanceDetail);
      this.madeChanges = true;           
      console.log(`Maintenance position deleted ${this.detailsList.length} => ${this.deletedDetailsList.length}!`, maintenanceDetail);
  }
  }

  onMaintenanceEdit(maintenanceDetail: IMaintenanceDetailsAngularModel)
  {
    this.selectedMaintenancePosition = maintenanceDetail;
  }

  onMaintenanceCancelEdit()
  {
    this.selectedMaintenancePosition = null;
  }
}
