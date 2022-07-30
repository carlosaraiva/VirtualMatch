import { Component, OnInit } from '@angular/core';
import { MembersService } from '../__services/members.service';
import { Member } from '../__models/Member';
import { Pagination } from '../__models/Pagination';
import { UserParams } from '../__models/UserParams';
import { User } from '../__models/User';
import { AccountService } from '../__services/account.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;
  userParams: UserParams;
  user: User;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];

  constructor(private membersService: MembersService, private accountService: AccountService) { 

    this.accountService.loggedUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(this.user);
    })

  }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.membersService.getMembers(this.userParams).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters() {
    this.userParams = new UserParams(this.user);
    this.loadMembers();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.loadMembers();
  }

}
