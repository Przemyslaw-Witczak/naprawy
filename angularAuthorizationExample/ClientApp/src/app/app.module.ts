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

import {
  IPublicClientApplication,
  PublicClientApplication,
  BrowserCacheLocation,
  LogLevel,
  InteractionType,
} from '@azure/msal-browser';
import {
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG,
  MsalInterceptorConfiguration,
  MSAL_GUARD_CONFIG,
  MsalGuardConfiguration,
  MsalBroadcastService,
  MsalService,
  MsalGuard,
  MsalRedirectComponent,
  MsalModule,
  MsalInterceptor,
} from '@azure/msal-angular';
import { ProfileComponent } from './profile/profile.component';


const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

const isIE =
  window.navigator.userAgent.indexOf('MSIE ') > -1 ||
  window.navigator.userAgent.indexOf('Trident/') > -1;

export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: '6ced2e7e-d6b4-4a5d-8f0c-57c70a9b2c8d',
      authority: 'https://login.microsoftonline.com/e9d9b795-b2a5-435c-97c9-77a382765404',
      redirectUri: 'https://localhost:44464',
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: isIE, // set to true for IE 11
    },
    system: {
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
      },
    },
  });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set(GRAPH_ENDPOINT, ['user.read']);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: ['user.read'],
    },
  };
}

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
    MaintenanceDetailEditComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
   /* ApiAuthorizationModule,*/
    MsalModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'czesci-dictionary', component: CzesciDictionaryComponent, canActivate: [MsalGuard] },
      { path: 'maintenances-dictionary', component: MaintenancesDictionaryComponent, canActivate: [MsalGuard] },
      { path: 'vehicles-list', component: VehiclesListComponent, canActivate: [MsalGuard] },
      { path: 'vehicles-list/:vehicleId', component: VehiclesListComponent, canActivate: [MsalGuard] },
      { path: 'vehicle-edit/:vehicleId', component: VehicleEditComponent, canActivate: [MsalGuard] },      
      { path: 'maintenances', component: MaintenancesComponent, canActivate: [MsalGuard] },      
      { path: 'maintenances/:vehicleId/:maintenanceId', component: MaintenancesComponent, canActivate: [MsalGuard] },      
      { path: 'maintenance-edit/:vehicleId/:maintenanceId', component: MaintenanceEditComponent, canActivate: [MsalGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [MsalGuard] }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService,
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
