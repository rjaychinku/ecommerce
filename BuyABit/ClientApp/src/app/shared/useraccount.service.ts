import { Injectable, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UseraccountService {

  constructor(private router: Router, private formbuilder: FormBuilder
              ,private http: HttpClient, @Inject('BASE_URL') public baseUrl: string
              ,private JwtHelperService: JwtHelperService) {
  }

  registrationFormModel = this.formbuilder.group({
    UserName: [''],
    Email: ['', [Validators.email, Validators.required]],
    FullName: [''],
    Password: [''],
    Passwords: this.formbuilder.group({
      Password: ['', [Validators.required, Validators.minLength(8)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  loginFormModel = this.formbuilder.group({
    Email: ['', Validators.required],
    Password: ['', [Validators.required, Validators.minLength(4)]]
  });

  checkoutFormModel = this.formbuilder.group({
    UserName: [''],
    Email: ['', [Validators.email, Validators.required]],
    FirstName: ['', Validators.required],
    LastName: ['', Validators.required],
    StreetAddress: ['', Validators.required],
    ZipPostalCode: ['', Validators.required],
    ApartmentSuite: ['']
  });

  comparePasswords(formGroup: FormGroup) {
    let confirmPswrdCtrl = formGroup.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (formGroup.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    //  console.log('Registration object ' + this.registrationFormModel.get('Passwords').get('Password').value);
    this.registrationFormModel.get('Password').setValue(this.registrationFormModel.get('Passwords').get('Password').value);
    this.registrationFormModel.get('UserName').setValue(this.registrationFormModel.get('Email').value);
    return this.http.post(this.baseUrl + 'ApplicationUser/Register', this.registrationFormModel.value);
  }

  login() {
    return this.http.post(this.baseUrl + 'ApplicationUser/Login', this.loginFormModel.value);
  }

  getProfile() {
    return this.http.get(this.baseUrl + 'ApplicationUser/GetProfile');
  }

  RefreshToken(tokens: any) {
    return this.http.post<any>(this.baseUrl + 'ApplicationUser/Refresh'
                , tokens
                , {
                    headers: new HttpHeaders({ "Content-Type": "application/json" })
                  }
    );
  }

  RevokeToken() {
    this.http.post(this.baseUrl + 'ApplicationUser/Revoke', "dddd")
      .subscribe(
        (result: any) => {
          console.log("revoked token!!");
          //this.stopRefreshTokenTimer();
          //this.user.next(null);
          this.removeToken();
          this.removeRefreshToken();
        },
        err => {
          console.log(err);
        }
      );

    this.goToLoginPage();
  }

  public getToken() {
    return localStorage.getItem('token');
  }

  getRefreshToken() {
    return localStorage.getItem('refreshtoken');
  }

  removeToken() {
    return localStorage.removeItem('token');
  }

  removeRefreshToken() {
    return localStorage.removeItem('refreshtoken');
  }

  isAuthenticated() {
      const token: string =  this.getToken();

      if (token && !this.JwtHelperService.isTokenExpired(token)) {
        return true;
      }    
      return false;
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  saveRefreshToken(refreshToken: string) {
    localStorage.setItem('refreshtoken', refreshToken);
  }

  goToLoginPage() {
    this.router.navigate(['account']);
  }

  logout() {
    //this.http.post<any>(`${this.baseUrl}/users/revoke-token`, {}, { withCredentials: true }).subscribe();
    return this.RevokeToken();
  }

  // refreshToken() {
  //     return this.http.post<any>(`${this.baseUrl}/users/refresh-token`, {}, { withCredentials: true })
  //         .pipe(map((user) => {
  //             this.userSubject.next(user);
  //             this.startRefreshTokenTimer();
  //             return user;
  //         }));
  // }

  // helper methods

  private refreshTokenTimeout;

  // private startRefreshTokenTimer() {
  //     // parse json object from base64 encoded jwt token
  //     const jwtToken = JSON.parse(atob(this.getToken().split('.')[1]));

  //     // set a timeout to refresh the token a minute before it expires
  //     const expires = new Date(jwtToken.exp * 1000);
  //     const timeout = expires.getTime() - Date.now() - (60 * 1000);
  //     this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
  // }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }

  setFormValues() {

  }

}
