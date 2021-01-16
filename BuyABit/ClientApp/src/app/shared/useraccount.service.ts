import { Injectable, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { BnNgIdleService } from 'bn-ng-idle';

@Injectable({
  providedIn: 'root'
})
export class UseraccountService {

  private static readonly BASE_URL = 'BASE_URL';

  constructor(private router: Router, private formbuilder: FormBuilder
    , private http: HttpClient, @Inject(UseraccountService.BASE_URL) public baseUrl: string
    , private JwtHelperService: JwtHelperService, private bnIdle : BnNgIdleService) {
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

  async RevokeToken() {
   try {
      const result = await this.http.post(this.baseUrl + 'ApplicationUser/Revoke', "dddd").toPromise();
      console.log("Revoked token!!");
   } catch (err) {
     console.log(err);
   }
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
    let token = this.getToken();

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
    this.bnIdle.stopTimer(); 
    this.removeToken();
    this.removeRefreshToken();
    this.router.navigate(['account']);
  }

  setFormValues() {

  }

}
