import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AuthHttpInterceptor, AuthModule } from '@auth0/auth0-angular';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { CzesciDictionaryComponent } from './czesci-dictionary/czesci-dictionary.component';
import { MaintenancesDictionaryComponent } from './maintenances-dictionary/maintenances-dictionary.component';
import { VehiclesListComponent } from './vehicles-list/vehicles-list.component';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { VehicleEditComponent } from './vehicle-edit/vehicle-edit.component';
import { MaintenancesComponent } from './maintenances/maintenances.component';
import { MaintenancesDetailsComponent } from './maintenances-details/maintenances-details.component';
import { MaintenanceEditComponent } from './maintenance-edit/maintenance-edit.component';
import { MaintenanceDetailEditComponent } from './maintenance-detail-edit/maintenance-detail-edit.component';




const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

const isIE =
  window.navigator.userAgent.indexOf('MSIE ') > -1 ||
  window.navigator.userAgent.indexOf('Trident/') > -1;



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CzesciDictionaryComponent,
    MaintenancesDictionaryComponent,
    VehiclesListComponent,
    VehicleDetailsComponent,
    VehicleEditComponent,
    MaintenancesComponent,
    MaintenancesDetailsComponent,
    MaintenanceEditComponent,
    MaintenanceDetailEditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
   
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'czesci-dictionary', component: CzesciDictionaryComponent},
      { path: 'maintenances-dictionary', component: MaintenancesDictionaryComponent},
      { path: 'vehicles-list', component: VehiclesListComponent},
      { path: 'vehicles-list/:vehicleId', component: VehiclesListComponent},
      { path: 'vehicle-edit/:vehicleId', component: VehicleEditComponent},      
      { path: 'maintenances', component: MaintenancesComponent},      
      { path: 'maintenances/:vehicleId/:maintenanceId', component: MaintenancesComponent},      
      { path: 'maintenance-edit/:vehicleId/:maintenanceId', component: MaintenanceEditComponent}      
    ]),
    AuthModule.forRoot({
      domain: 'dev-kadj1mmr6t5mt0pv.eu.auth0.com',
      clientId: 'kue1IOBTqYQM8xjqDRymGmYo74FWmro8',
      authorizationParams: {
        redirect_uri: 'https://localhost:44464', 
        client_secret: 'xQd1QHvDsRzKGnD3Do-68bMVy1i_9CVQ8VuKJXQszn3coWuhLcbIe55pDz-3hFuF'
      }
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true
    }    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
