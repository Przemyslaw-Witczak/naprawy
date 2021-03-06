import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { CzesciDictionaryAngularModel } from './CzesciDictionaryAngularModel';

@Component({
  selector: 'app-czesci-dictionary',
  templateUrl: './czesci-dictionary.component.html',
  styleUrls: ['./czesci-dictionary.component.css']
})
export class CzesciDictionaryComponent implements OnInit {

  public czesciArray: CzesciDictionaryAngularModel[] = [];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CzesciDictionaryAngularModel[]>(baseUrl + 'PartsDictionary').subscribe(result => {
      this.czesciArray = result;
    }, error => console.error(error));
    console.log("Adres do kontrollera: "+baseUrl);
  }

  ngOnInit(): void {
  }

}

