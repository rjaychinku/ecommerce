import { Component, Inject, OnInit } from '@angular/core';
import { UseraccountService } from '../shared/useraccount.service'
import { Router } from '@angular/router';
import { BnNgIdleService } from 'bn-ng-idle';


@Component({
  selector: 'app-useraccount',
  templateUrl: './useraccount.component.html',
  styleUrls: ['./useraccount.component.css']
})
export class UseraccountComponent implements OnInit {

  private static readonly Application_Settings = 'Application_Settings';

  constructor(private useraccountService: UseraccountService, private router: Router
    , private bnIdle: BnNgIdleService
    , @Inject(UseraccountComponent.Application_Settings) private _Application_Settings) { }

  loginFormModel = {
    UserName: '',
    Password: ''
  }

  ngOnInit() {
    this.useraccountService.registrationFormModel.reset();
    this.useraccountService.loginFormModel.reset();
  }

  private onLoginSubmit() {

    this.useraccountService.login().subscribe(
      (result: any) => {
        console.log("succeeded Login!!");
        this.startTrackingIdleUser();
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

  startTrackingIdleUser() {
    console.log("started Tracking Idle User at " + this._Application_Settings.USER_IDLE_SECONDS + " secs");
    this.bnIdle.startWatching(this._Application_Settings.USER_IDLE_SECONDS).subscribe((isTimedOut: boolean) => {
      if (isTimedOut) {
        this.bnIdle.stopTimer();
        this.useraccountService.logout();
        console.log("stopped Tracking Idle User....");
      }
      else {
        console.log('not timed out yet...')
      }
    });
  }

}
