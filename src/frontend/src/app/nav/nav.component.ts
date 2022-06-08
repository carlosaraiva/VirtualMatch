import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  loggedUser: User;

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.accountService.loggedUser$.subscribe(
      loggedUser =>  {
        this.loggedUser = loggedUser;
      }
    );
  }

  login() {
    this.accountService.login(this.model).subscribe(
      _ => {
        this.router.navigateByUrl('/members');
      }
    );
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
