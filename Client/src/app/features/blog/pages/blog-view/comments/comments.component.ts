import {Component, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../../../../../core/services/auth.service";
import {User} from "../../../../../core/models/user";
import {EMPTY, Observable, tap} from "rxjs";
import {CommentApiService} from "../../../services/comment-api.service";
import {Comment} from "../../../models/comment";

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent {

  @Input() public set postId (value: number | undefined) {
    if (value && value != 0) {
      this._postId = value;
      this.comments$ = this.commentApi.GetComments(value)
        .pipe(tap(x => this.CommentsLoaded.emit(x.length)));
    }
  }
  @Output() public CommentsLoaded = new EventEmitter<number>();

  public _postId?: number;
  public user$?: Observable<User | null>;
  public comments$: Observable<Comment[]> = EMPTY;


  constructor(private auth: AuthService, private commentApi: CommentApiService) {
    this.user$ = auth.user$;
  }

  public onCommentSend(comment: string): void {
    if (this._postId)
    this.commentApi.PostComment(this._postId, comment).subscribe(() => this.postId = this._postId);
  }

}
