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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CzesciDictionaryComponent,
    MaintenancesDictionaryComponent,
    VehiclesListComponent
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
      { path: 'vehicles-list', component: VehiclesListComponent, canActivate: [AuthorizeGuard] }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
