import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {UntilDestroy, untilDestroyed} from "@ngneat/until-destroy";
import {PostApiService} from "../../services/post-api.service";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-blog-view',
  templateUrl: './blog-view.component.html',
  styleUrls: ['./blog-view.component.scss']
})
@UntilDestroy()
export class BlogViewComponent implements OnInit {

  public post?: Observable<string>

  constructor(private route: ActivatedRoute, private postApi: PostApiService) { }

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(untilDestroyed(this))
      .subscribe(params => {
          const id = +(params.get('id') ?? -1);
          if (id != -1) {
            this.post = this.postApi.GetPostById(id).pipe(map(x => x.content))
          }
        }
      );
  }

}
