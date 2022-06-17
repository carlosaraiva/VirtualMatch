import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../__models/Login';
import { User } from '../__models/User';
import { map } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  BUFFER_SIZE: number = 1;
  USER_STORAGE_NAME: string = 'logged_user';

  apiUrl = environment.apiUrl;
  private loggedUser = new ReplaySubject<User>(this.BUFFER_SIZE);
  loggedUser$ = this.loggedUser.asObservable();

  constructor(private http: HttpClient) { }

  login(model: Login) {
    return this.http.post(this.apiUrl + 'accounts/login', model).pipe(
      map((response: User) => {
        const user = response;

        if(user) {
          this.setLoggedUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.apiUrl + 'accounts', model).pipe(
      map((user: User) => {
        if(user) {
          this.setLoggedUser(user);
        }

        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem(this.USER_STORAGE_NAME);
    this.loggedUser.next(null);
  }

  setLoggedUser(user: User) {
    console.log(user);
    localStorage.setItem(this.USER_STORAGE_NAME, JSON.stringify(user));
    this.loggedUser.next(user);
  }
}
