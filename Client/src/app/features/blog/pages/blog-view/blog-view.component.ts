import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {UntilDestroy, untilDestroyed} from "@ngneat/until-destroy";
import {PostApiService} from "../../services/post-api.service";
import {Observable, share} from "rxjs";
import {Post} from "../../models/post";

@Component({
  selector: 'app-blog-view',
  templateUrl: './blog-view.component.html',
  styleUrls: ['./blog-view.component.scss']
})
@UntilDestroy()
export class BlogViewComponent implements OnInit {

  public post$?: Observable<Post>;
  // public title$?: Observable<string | undefined>;
  // public post

  constructor(private route: ActivatedRoute, private postApi: PostApiService) { }

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(untilDestroyed(this))
      .subscribe((params: any) => {
          const id = +(params.get('id') ?? -1);
          if (id > 0) {
            this.post$ = this.postApi.GetPostById(id).pipe(share());
            // this.post$ = result.pipe(map(x => x.content));
            // this.title$ = result.pipe(map(x => x.title));
          }
        }
      );
  }

}
