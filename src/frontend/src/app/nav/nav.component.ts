import { Component, OnInit } from '@angular/core';
import { Login, newLogin } from '../__models/Login';
import { AccountService } from '../__services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: Login = newLogin();

  constructor(private accountsService: AccountService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountsService.login(this.model);
  }

}
