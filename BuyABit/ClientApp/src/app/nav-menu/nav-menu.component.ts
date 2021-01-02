import { Component } from '@angular/core';
import { UseraccountService } from '../shared/useraccount.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent{
  isExpanded = false;

  constructor (private useraccountService: UseraccountService)
  {

  }

  collapse() {
    this.isExpanded = false;
  }

  logout() {
    this.useraccountService.logout();
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
