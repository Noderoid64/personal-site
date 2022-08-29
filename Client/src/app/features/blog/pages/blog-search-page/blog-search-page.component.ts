import { Component } from '@angular/core';
import {Observable, of, tap} from "rxjs";
import {PostApiService} from "../../services/post-api.service";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-blog-search-page',
  templateUrl: './blog-search-page.component.html',
  styleUrls: ['./blog-search-page.component.scss']
})
export class BlogSearchPageComponent {

  public posts$?: Observable<any>;
  public searchControl = new FormControl();

  private searchValueKey = "searchValueKey";
  private searchResultKey = "searchResultKey";

  constructor(private postApi: PostApiService) {
    const searchValue = localStorage.getItem(this.searchValueKey);
    const searchResult = localStorage.getItem(this.searchResultKey);
    if(searchValue && searchResult) {
      this.searchControl.setValue(searchValue);
      const results = JSON.parse(searchResult);
      this.posts$ = of(results);
    }
  }

  public onSearch(): void {
    const searchValue = this.searchControl.value;
    if (searchValue) {
      localStorage.setItem(this.searchValueKey, searchValue);
      this.posts$ = this.postApi.FindPosts(searchValue).pipe(tap(x => {
        if(x) {
          localStorage.setItem(this.searchResultKey, JSON.stringify(x));
        }
      }));
    }

  }

}
