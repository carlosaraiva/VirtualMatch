import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Member } from '../__models/member';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AccountService } from './account.service';
import { User } from '../__models/user';
import { PaginatedResult } from '../__models/Pagination';
import { UserParams } from '../__models/UserParams';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('logged_user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  user: User;
  USER_STORAGE_NAME: string = 'logged_user';
  paginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.loggedUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }

  getMembers(userParams: UserParams) {
    var response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return this.getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http)
      .pipe(map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      }))
  }

  getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }

  getPaginatedResult<T>(url, params, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getMember(username: String) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/photo/' + photoId);
  }
  
}