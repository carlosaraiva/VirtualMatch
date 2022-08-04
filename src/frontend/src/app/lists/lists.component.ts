import { Component, OnInit } from '@angular/core';
import { Member } from '../__models/member';
import { Pagination } from '../__models/pagination';
import { MembersService } from '../__services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Partial<Member[]>;
  predicate = 'liked';
  pageNumber = 1;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadLikes();
  }
  
  loadLikes() {
    this.memberService.getLikes(this.predicate).subscribe(response => {
      this.members = response;
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadLikes();
  }

}