import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UseraccountService } from '../shared/useraccount.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import {HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private useraccountService: UseraccountService, private router: Router
            ,private JwtHelperService: JwtHelperService,  private http: HttpClient) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const jwtToken = this.useraccountService.getToken();

    if (!jwtToken) {
      this.useraccountService.goToLoginPage();
      return false;
    }

    if (this.JwtHelperService.isTokenExpired(jwtToken)) {
      const isRefreshSuccess = await this.tryRefreshingTokens(jwtToken);

      if (!isRefreshSuccess) {
        //this.router.navigate(['account'], { queryParams: { returnUrl: state.url } });
        this.useraccountService.removeToken();
        this.useraccountService.goToLoginPage();
      }
      return isRefreshSuccess;
    }

    return true;
  }

  private async tryRefreshingTokens(token: string): Promise<Promise<boolean> | boolean> {
    // Try refreshing tokens using refresh token
    const refreshToken = this.useraccountService.getRefreshToken();
    const tokens = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
    let isRefreshSuccess = false;

    try
    {    
        let result = await this.useraccountService.RefreshToken(tokens).toPromise();

        if (result && result.accessToken && result.refreshToken)
        {        
          console.log("RefreshToken() worked!!");
          const newToken = result.accessToken;
          const newRefreshToken = result.refreshToken;
          this.useraccountService.saveToken(newToken);
          this.useraccountService.saveRefreshToken(newRefreshToken);
          isRefreshSuccess = true;
        }
    }
    catch (err) {      
      console.log(err);
      isRefreshSuccess = false;
    }

    return isRefreshSuccess;
  }
}
