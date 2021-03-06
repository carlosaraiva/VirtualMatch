import { Component, OnInit } from '@angular/core';
import { MembersService } from '../__services/members.service';
import { Member } from '../__models/Member';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];

  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.membersService.getMembers().subscribe(members => {
      this.members = members;
    })
  }

}
