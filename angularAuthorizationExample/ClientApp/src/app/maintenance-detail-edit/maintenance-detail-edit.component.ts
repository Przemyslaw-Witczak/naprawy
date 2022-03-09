import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
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
  @Output() public onSave = new EventEmitter();
    
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
    //ToDo: Creeate copy of edited element
  }

  onSubmit()
  {
    //this.maintenancePosition = null;
    //ToDo: Sprawdzić, czy element jest zedytowany    
    this.onSave.emit(true);
  }
  

  goBack()
  {
    this.maintenancePosition = null;
    this.onSave.emit(false);
  }

  calculateCost()
  {
    if (this.maintenancePosition)
      this.maintenancePosition.cost = this.maintenancePosition.price * this.maintenancePosition.quantity;
  }

}


