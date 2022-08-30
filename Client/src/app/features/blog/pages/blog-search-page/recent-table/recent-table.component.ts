import { Component, OnInit } from '@angular/core';
import {PostApiService} from "../../../services/post-api.service";
import {Observable} from "rxjs";
import {PostRecent} from "../../../models/post-recent";

@Component({
  selector: 'app-recent-table',
  templateUrl: './recent-table.component.html',
  styleUrls: ['./recent-table.component.scss']
})
export class RecentTableComponent implements OnInit {

  public displayedColumns: string[] = ['authorPict', 'authorNick', 'title', 'createdAt'];
  public dataSource = [];
  public posts$: Observable<PostRecent[]>;

  constructor(private postApi: PostApiService) {
    this.posts$ = postApi.GetRecentPosts();
  }

  ngOnInit(): void {
  }

}
