import { Component } from '@angular/core';
import {Observable, of, tap} from "rxjs";
import {PostApiService} from "../../services/post-api.service";
import {FormControl} from "@angular/forms";
import {SearchStorageService} from "../../services/search-storage.service";

@Component({
  selector: 'app-blog-search-page',
  templateUrl: './blog-search-page.component.html',
  styleUrls: ['./blog-search-page.component.scss']
})
export class BlogSearchPageComponent {

  public posts$?: Observable<any>;
  public searchControl = new FormControl();

  constructor(private postApi: PostApiService, private searchStorage: SearchStorageService) {
    const searchValue = searchStorage.getSearchValue()
    const searchResult = searchStorage.getSearchResult()
    if(searchValue && searchResult) {
      this.searchControl.setValue(searchValue);
      const results = JSON.parse(searchResult);
      this.posts$ = of(results);
    }
  }

  public onSearch(): void {
    const searchValue = this.searchControl.value;
    if (searchValue) {
      this.searchStorage.setSearchValue(searchValue);
      this.posts$ = this.postApi.FindPosts(searchValue).pipe(tap(x => {
        if(x) {
          this.searchStorage.setSearchResult(JSON.stringify(x));
        }
      }));
    }

  }

}
