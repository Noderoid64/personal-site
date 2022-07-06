import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../../../core/services/auth.service";
import {Observable, take} from "rxjs";
import {User} from "../../../../core/models/user";
import {PostApiService} from "../../services/post-api.service";
import {Post} from "../../models/post";

@Component({
  selector: 'app-blog-main-page',
  templateUrl: './blog-main-page.component.html',
  styleUrls: ['./blog-main-page.component.scss']
})
export class BlogMainPageComponent implements OnInit {

  public isUserLogged$: Observable<User | null>;
  public posts$: Observable<Post[]>;

  constructor(private authService: AuthService, private postApi: PostApiService) {
    this.isUserLogged$ = authService.user$;
    this.posts$ = postApi.GetPostsByProfileId();
  }

  ngOnInit(): void {
  }

}
