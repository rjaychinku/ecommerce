import { Component, Inject, OnInit } from '@angular/core';
import { UseraccountService } from '../shared/useraccount.service'
import { Router } from '@angular/router';
import { BnNgIdleService } from 'bn-ng-idle';
import { Console } from 'console';

@Component({
  selector: 'app-useraccount',
  templateUrl: './useraccount.component.html',
  styleUrls: ['./useraccount.component.css']
})
export class UseraccountComponent implements OnInit {

  private static readonly USER_IDLE_SECONDS = 'USER_IDLE_SECONDS';

  constructor(private useraccountService: UseraccountService, private router: Router
              ,private bnIdle: BnNgIdleService
              ,@Inject(UseraccountComponent.USER_IDLE_SECONDS) private userIdleSeconds: number) { }

  loginFormModel = {
    UserName: '',
    Password: ''
  }

  private ngOnInit() {
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
    console.log("started Tracking Idle User at " + this.userIdleSeconds + " secs");
    this.bnIdle.startWatching(this.userIdleSeconds).subscribe((isTimedOut: boolean) => {
      if (isTimedOut) {
        this.bnIdle.stopTimer(); 
        this.useraccountService.logout();
        console.log("stopped Tracking Idle User....");
      }
      else
      {
        console.log('not timed out yet...')
      }
    });
  }

}
