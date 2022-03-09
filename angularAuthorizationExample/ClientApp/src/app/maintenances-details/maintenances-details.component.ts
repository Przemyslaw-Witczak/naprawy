import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, Input, EventEmitter, OnInit, Output } from '@angular/core';
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
  @Output() public isEditingPositionEvent = new EventEmitter();
  @Output() public onChange = new EventEmitter();
  public changesNeedSave: boolean = false;
  public selectedMaintenancePosition: IMaintenanceDetailsAngularModel | null = null;
  public detailsList: IMaintenanceDetailsAngularModel[] = [];
  public deletedDetailsList: IMaintenanceDetailsAngularModel[] = [];
  
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private route: ActivatedRoute) {     
  }

  ngOnInit(): void { 
    console.log(`Parent maintenance id=${this.parentMaintenanceId}`);
    if (this.parentMaintenanceId!=undefined)
    {
      this.http.get<IMaintenanceDetailsAngularModel[]>(this.baseUrl + 'MaintenancesDetails/'+this.parentMaintenanceId).subscribe(result => {
        this.detailsList = result;  
        this.deletedDetailsList = [];
        this.onChange.emit(false);
      }, error => console.error(error));
    }
  }

  onMaintenanceCreate()
  {
    this.selectedMaintenancePosition = <IMaintenanceDetailsAngularModel>{
      quantity: 1.0,
      idMaintenance: this.parentMaintenanceId
    }
    this.detailsList.push(this.selectedMaintenancePosition);
    this.onChange.emit(true);
    this.isEditingPositionEvent.emit(true);
  }

  onMaintenanceDelete(maintenanceDetail: IMaintenanceDetailsAngularModel)
  {
    if(confirm(`Czy na pewno chcesz usunąć pozycję ${maintenanceDetail.maintenance?.name} ${maintenanceDetail.part?.name} ${maintenanceDetail.description}?`)) {
      maintenanceDetail.deleted = true;            
      this.detailsList.splice(this.detailsList.indexOf(maintenanceDetail),1);
      this.deletedDetailsList.push(maintenanceDetail);
      this.onChange.emit(true); 
      this.changesNeedSave = true;          
      console.log(`Maintenance position deleted ${this.detailsList.length} => ${this.deletedDetailsList.length}!`, maintenanceDetail);
  }
  }

  onMaintenanceEdit(maintenanceDetail: IMaintenanceDetailsAngularModel)
  {
    this.selectedMaintenancePosition = maintenanceDetail;
    //Send event to parent, notify i'm in editing mode.
    this.isEditingPositionEvent.emit(true);
  }

  onMaintenanceCancelEdit()
  {
    this.selectedMaintenancePosition = null;
    this.isEditingPositionEvent.emit(false);
  }

  onPositionSave(value: boolean)
  {
    this.selectedMaintenancePosition = null;
    this.onChange.emit(value);
    this.isEditingPositionEvent.emit(false);
    this.changesNeedSave = true;
  }

  // onChanged(value: boolean)
  // {
  //   if (value)
  //     this.changesNeedSave = true;
  // }
}
