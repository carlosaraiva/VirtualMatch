import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Login, newLogin } from '../__models/Login';
import { User } from '../__models/User';
import { AccountService } from '../__services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: Login = newLogin();

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe(
      response => {
        console.log(response);
      },
      error => {
        console.log(error);
      }
    );
  }

  logout() {
    this.accountService.logout();
  }

}
