import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Inject, Input, OnInit } from '@angular/core';
import { CzesciDictionaryAngularModel } from '../czesci-dictionary/CzesciDictionaryAngularModel';
import { CzynnosciDictionaryAngularModel } from '../maintenances-dictionary/CzynnosciDictionaryAngularModel';
import { IMaintenanceDetailsAngularModel } from '../maintenances/MaintenanceDetailsAngularModel';

@Component({
  selector: 'app-maintenance-detail-edit',
  templateUrl: './maintenance-detail-edit.component.html',
  styleUrls: ['./maintenance-detail-edit.component.css']
})
export class MaintenanceDetailEditComponent implements OnInit {
  @Input('editedMaintenancePosition') public maintenancePosition : IMaintenanceDetailsAngularModel | null = null;
  @Output() public childEvent = new EventEmitter();
  fireEvent()
  {
    this.childEvent.emit('Message from component');
  }
  public maintenancesDictionary: CzynnosciDictionaryAngularModel[] = [];  
  public partsDictionary: CzesciDictionaryAngularModel[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    http.get<CzesciDictionaryAngularModel[]>(baseUrl + 'PartsDictionary').subscribe(result => {
    this.partsDictionary = result;
  }, error => console.error(error));

  http.get<CzynnosciDictionaryAngularModel[]>(baseUrl + 'MaintenancesDictionary').subscribe(result => {
    this.maintenancesDictionary = result;
  }, error => console.error(error));  
}

  ngOnInit(): void {
    
  }

  onSubmit()
  {
    this.maintenancePosition = null;
  }

  changeMaintenance(event:Event)
  {

  }

  goBack()
  {

  }
}
function Output() {
  throw new Error('Function not implemented.');
}

