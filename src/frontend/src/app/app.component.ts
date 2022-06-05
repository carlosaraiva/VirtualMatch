import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './__models/User';
import { AccountService } from './__services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'Virtual Match';
  users: any;

  constructor(private http: HttpClient, private accountService: AccountService) {}
  
  ngOnInit(): void {
    this.getUsers();
    this.setLoggedUser();
  }

  setLoggedUser() {
    const user: User = JSON.parse(localStorage.getItem(this.accountService.USER_STORAGE_NAME));
    this.accountService.setLoggedUser(user);
  }

  getUsers() {
    this.http.get("https://localhost:44307/api/Users").subscribe(response => {
      this.users = response;
      console.log(this.users);
    },
    error=> {
      console.log(error);
    });
  }

}
