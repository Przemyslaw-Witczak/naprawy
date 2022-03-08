import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { CzesciDictionaryComponent } from './czesci-dictionary/czesci-dictionary.component';
import { MaintenancesDictionaryComponent } from './maintenances-dictionary/maintenances-dictionary.component';
import { VehiclesListComponent } from './vehicles-list/vehicles-list.component';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { VehicleEditComponent } from './vehicle-edit/vehicle-edit.component';
import { MaintenancesComponent } from './maintenances/maintenances.component';
import { MaintenancesDetailsComponent } from './maintenances-details/maintenances-details.component';
import { MaintenanceEditComponent } from './maintenance-edit/maintenance-edit.component';
import { MaintenanceDetailEditComponent } from './maintenance-detail-edit/maintenance-detail-edit.component';

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
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'czesci-dictionary', component: CzesciDictionaryComponent, canActivate: [AuthorizeGuard] },
      { path: 'maintenances-dictionary', component: MaintenancesDictionaryComponent, canActivate: [AuthorizeGuard] },
      { path: 'vehicles-list', component: VehiclesListComponent, canActivate: [AuthorizeGuard] },
      { path: 'vehicles-list/:vehicleId', component: VehiclesListComponent, canActivate: [AuthorizeGuard] },
      { path: 'vehicle-edit/:vehicleId', component: VehicleEditComponent, canActivate: [AuthorizeGuard] },      
      { path: 'maintenances', component: MaintenancesComponent, canActivate: [AuthorizeGuard] },      
      { path: 'maintenances/:vehicleId/:maintenanceId', component: MaintenancesComponent, canActivate: [AuthorizeGuard] },      
      { path: 'maintenance-edit/:vehicleId/:maintenanceId', component: MaintenanceEditComponent, canActivate: [AuthorizeGuard] }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
