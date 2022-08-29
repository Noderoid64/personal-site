import { Component } from '@angular/core';
import {AuthService} from "../../../../core/services/auth.service";
import {Observable } from "rxjs";
import {User} from "../../../../core/models/user";
import {PostApiService} from "../../services/post-api.service";
import {Post} from "../../models/post";
import {SearchStorageService} from "../../services/search-storage.service";

@Component({
  selector: 'app-blog-main-page',
  templateUrl: './blog-main-page.component.html',
  styleUrls: ['./blog-main-page.component.scss']
})
export class BlogMainPageComponent {

  public isUserLogged$: Observable<User | null>;
  public posts$: Observable<Post[]>;

  constructor(private authService: AuthService, private postApi: PostApiService, private searchStorage: SearchStorageService) {
    this.isUserLogged$ = authService.user$;
    this.posts$ = postApi.GetPostsByProfileId();
  }

  public onPostBrowse(): void {
    this.searchStorage.reset();
  }

}
