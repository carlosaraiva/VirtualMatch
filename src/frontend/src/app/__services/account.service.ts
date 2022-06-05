import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../__models/Login';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  apiUrl = 'https://localhost:44307/api/'
  logged: Boolean;

  constructor(private http: HttpClient) { }

  login(model: Login) {
    return this.http.post(this.apiUrl + 'accounts/login', model).subscribe(
      response => {
        console.log(response);
        this.logged = true;
      },
      error => {
        console.log(error);
        this.logged = false;
      }
    );
  }
}
