import { Component, OnInit } from '@angular/core';
import { UseraccountService } from '../shared/useraccount.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-useraccount',
  templateUrl: './useraccount.component.html',
  styleUrls: ['./useraccount.component.css']
})
export class UseraccountComponent implements OnInit {

  constructor(public useraccountService: UseraccountService, private router: Router) { }

  loginFormModel = {
    UserName: '',
    Password: ''
  }

  ngOnInit() {
    this.useraccountService.registrationFormModel.reset();
    this.useraccountService.loginFormModel.reset();
  }

  onLoginSubmit() {
    console.log("ckikced!");
    this.useraccountService.login().subscribe(
      (result: any) => {
        console.log("succeeded Login!!");
        this.useraccountService.saveToken(result['token']);
        this.useraccountService.saveRefreshToken(result['refreshToken']);
        this.router.navigate(["home"]);
      },
      err => {
        console.log(err);
      }
    );
  }

  onRegistrationSubmit() {
    this.useraccountService.register().subscribe(
      (result: any) => {
        if (result.succeeded) {
          console.log("succeeded Reg!!");
          this.useraccountService.registrationFormModel.reset();
          this.router.navigate(["home"]);
          //this.toastr.success('New user created!', 'Registration successful.');
        } else {
          result.errors.forEach(element => {
            console.log(element.description);
          });
        }
      },
      err => {
        err.errors.forEach(element => {
          console.log(element.description);
        });
      }
    );
  }
}
