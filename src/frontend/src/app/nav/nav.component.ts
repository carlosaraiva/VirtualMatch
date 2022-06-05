import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe(
      response => {
        this.router.navigateByUrl('/members');
      },
      error => {
        console.log(error);
      }
    );
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
