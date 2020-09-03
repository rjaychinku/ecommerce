import { Injectable, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UseraccountService {
  baseUrl : string;

  constructor(private formbuilder: FormBuilder, private http: HttpClient, @Inject('BASE_URL') _baseUrl: string)
  {
    this.baseUrl = _baseUrl;
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
    console.log('Registration object ' + this.registrationFormModel.get('Passwords').get('Password').value);
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

  getToken() {
    return localStorage.getItem('token');
  }

  removeToken() {
    return localStorage.removeItem('token');
  }

  isAuthenticated() {
    return this.getToken() ? true : false;
  }

  saveToken(token) {
    localStorage.setItem('token', token)
  }

  setFormValues() {
 
  }

}
