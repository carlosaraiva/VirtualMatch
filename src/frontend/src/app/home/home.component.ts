import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    //this.http.get("https://localhost:44307/api/users").subscribe(
      this.http.get(environment.apiUrl + "/users").subscribe(
      users => this.users = users
    );
  }

  cancelRegister(event: boolean) {
    this.registerMode = event;
  }

}
