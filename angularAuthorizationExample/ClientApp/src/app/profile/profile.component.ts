import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

type ProfileType = {
  givenName?: string,
  surname?: string,
  userPrincipalName?: string,
  id?: string
}

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile!: ProfileType;

  constructor(
    private http: HttpClient,
    private authService: MsalService
  ) { }

  ngOnInit() {
    this.getProfile();
  }

  getProfile() {
    const activeAccount = this.authService.instance.getActiveAccount();

    if (activeAccount) {

      this.authService.instance.acquireTokenSilent({
        account: activeAccount,
        scopes: ['user.read'],
      }).then(response => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${response.accessToken}`,
        });

        this.http.get(GRAPH_ENDPOINT, { headers: headers })
          .subscribe(profile => {
            this.profile = profile;
          });
      });


    }
  }
}
