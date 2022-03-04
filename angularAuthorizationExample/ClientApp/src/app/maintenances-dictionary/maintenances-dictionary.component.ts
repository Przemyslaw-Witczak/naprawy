import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { CzynnosciDictionaryAngularModel } from './CzynnosciDictionaryAngularModel';

@Component({
  selector: 'app-maintenances-dictionary',
  templateUrl: './maintenances-dictionary.component.html',
  styleUrls: ['./maintenances-dictionary.component.css']
})
export class MaintenancesDictionaryComponent implements OnInit {

  public czynnosciArray: CzynnosciDictionaryAngularModel[] = [];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CzynnosciDictionaryAngularModel[]>(baseUrl + 'MaintenancesDictionary').subscribe(result => {
      this.czynnosciArray = result;
    }, error => console.error(error));
    console.log("Adres do kontrollera: "+baseUrl);
  }

  ngOnInit(): void {
  }

}

